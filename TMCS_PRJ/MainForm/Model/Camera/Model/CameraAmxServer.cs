using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public class CameraAmxServer
    {
        public event EventHandler AmxConnected;

        private string _amxIp;
        private int _amxPort;

        TcpClient _client;
        NetworkStream _stream;

        private bool isSavePresetClick = false;

        public CameraAmxServer(string amxIp, int amxPort) 
        {
            _amxIp = amxIp;
            _amxPort = amxPort;
        }

        public async Task ConnectAmxServerAsync()
        {
            try
            {
                _client = new TcpClient();

                // 비동기적으로 서버에 연결을 시도합니다.
                await _client.ConnectAsync(_amxIp, _amxPort);

                // 연결에 성공하면 스트림을 얻습니다.
                _stream = _client.GetStream();
                AmxConnected?.Invoke(this, EventArgs.Empty);
                ReadDataAsync(_stream);
                //return _stream; // 스트림 반환
            }
            catch (Exception ex)
            {
                Debug.WriteLine("오류 발생: " + ex.Message);
                
            }
        }

        private TaskCompletionSource<byte[]> _tcs;

        public async Task<byte[]> GetCameraPosition()
        {
            isSavePresetClick = true;

            _tcs = new TaskCompletionSource<byte[]>();

            byte[] position = await _tcs.Task;

            return position;
        }

        private async Task ReadDataAsync(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead > 11)
                {
                    // 90 50 패턴 찾기
                    for (int i = 0; i < bytesRead - 1; i++)
                    {
                        if (buffer[i] == 0x90 && buffer[i + 1] == 0x50)
                        {
                            // 패턴 찾음
                            if (bytesRead > 11 && isSavePresetClick)
                            {
                                byte[] tmpBuffer = new byte[9];
                                Array.Copy(buffer, i + 2, tmpBuffer, 0, 9);
                                _tcs.TrySetResult(tmpBuffer);
                                isSavePresetClick = false;
                                break; // 반복문 탈출
                            }
                        }
                    }
                }

                //    byte[] buffer = new byte[1024];
                //while (true)
                //{

                //    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                //    if(bytesRead > 11 && isSavePresetClick && buffer[0] == 0x90 && buffer[1] == 0x50)
                //    {
                //        byte[] tmpBuffer = new byte[9];
                //        Array.Copy(buffer, 2, tmpBuffer, 0, 9);

                //        _tcs.TrySetResult(tmpBuffer.Take(9).ToArray());
                //        isSavePresetClick = false;
                //    }
                //    string tmp = BitConverter.ToString(buffer, 0, bytesRead);        
                //}
            }
        }

        public NetworkStream GetStream()
        {

            return _stream;
        }

        public bool GetStatus()
        {
            // TcpClient.Connected와 소켓의 Poll 메서드를 사용하여 연결 상태를 확인
            if (_client != null && _client.Connected)
            {
                // Poll 메서드를 사용하여 연결 상태를 더 정확히 확인
                bool part1 = _client.Client.Poll(1000, SelectMode.SelectRead);
                bool part2 = (_client.Client.Available == 0);
                if (part1 && part2)
                    return false; // 소켓이 닫힌 상태
                else
                    return true; // 소켓이 연결된 상태
            }
            
            return false;
        }
    }
}
