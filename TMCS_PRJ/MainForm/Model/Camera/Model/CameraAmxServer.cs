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
            ConnectAmxServerAsync();
        }

        public async Task ConnectAmxServerAsync()
        {
            const int maxAttempts = 3;
            int attemptCount = 0;

            while (attemptCount < maxAttempts)
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
                    return; // 성공하면 메서드 종료
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"연결 시도 {attemptCount + 1}: 오류 발생 - {ex.Message}");

                    attemptCount++;

                    if (attemptCount >= maxAttempts)
                    {
                        Debug.WriteLine("최대 연결 시도 횟수에 도달했습니다.");
                        break;
                    }

                    // 잠시 기다린 후 다시 시도합니다.
                    await Task.Delay(500);
                }
            }
        }

        public async Task PanTiltAsync(int cameraId,int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            if (_stream == null)
            {
                return;
            }

            byte byteCameraId = (byte)(128 + cameraId);
            byte bytePanSpeed = (byte)panSpeed;
            byte bytetiltSpeed = (byte)tiltSpeed;
            byte bytepanDirection = (byte)panDir;
            byte bytetiltDirection = (byte)tiltDir;

            byte[] command = new byte[] { byteCameraId, 0x01, 0x06, 0x01, bytePanSpeed, bytetiltSpeed, bytepanDirection, bytetiltDirection, 0xFF };

            _stream.Write(command, 0, command.Length);
        }

        private TaskCompletionSource<byte[]> returnMsg;
        private byte saveCameraId;
        
        public async Task<byte[]> SavePresetAsync(int cameraId)
        {
            byte byteCameraId = (byte)(128 + cameraId);
            saveCameraId = (byte)((16 * cameraId) + 128);
            byte[] command = new byte[] { byteCameraId, 0x09, 0x06, 0x12, 0xFF };
            _stream.Write(command, 0, command.Length);

            isSavePresetClick = true;
            //tt = 0x90;
            returnMsg = new TaskCompletionSource<byte[]>();

            byte[] position = await returnMsg.Task;

            return position;
        }

        public async Task LoadPresetAsync(int cameraId, byte[] presetPosition)
        {
            byte byteCameraId = (byte)(128 + cameraId);
            List<byte> message = new List<byte> { byteCameraId };
            message.AddRange(new byte[] { 0x01, 0x06, 0x02, 0x18, 0x18 });
            message.AddRange(presetPosition);
            message.AddRange(new byte[] { 0xFF });

            byte[] command = message.ToArray();
            _stream.Write(command, 0, command.Length);
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
                        //프리셋 저장한값 가져오기
                        if (buffer[i] == saveCameraId && buffer[i + 1] == 0x50)
                        {
                            // 패턴 찾음
                            if (bytesRead > 11 && isSavePresetClick)
                            {
                                Debug.WriteLine("찾음");
                                byte[] tmpBuffer = new byte[9];
                                Array.Copy(buffer, i + 2, tmpBuffer, 0, 9);
                                returnMsg.TrySetResult(tmpBuffer);
                                isSavePresetClick = false;
                                break; // 반복문 탈출
                            }
                        }
                    }
                }
            }
        }

        private async Task<bool> GetStatusAsync()
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



            //var socket = _client.Client;

            //if (socket == null)
            //    return false;

            //// 소켓이 연결되어 있지 않다면 바로 false를 반환합니다.
            //if (!socket.Connected)
            //    return false;

            //try
            //{
            //    // 비동기 작업을 위한 SocketAsyncEventArgs 인스턴스를 생성합니다.
            //    var args = new SocketAsyncEventArgs();
            //    var buffer = new byte[1];
            //    args.SetBuffer(buffer, 0, 0); // 0 바이트 길이의 데이터를 설정합니다.

            //    // 비동기 송수신 작업을 시작합니다.
            //    var isReceiveAsync = socket.ReceiveAsync(args);
            //    var isSendAsync = socket.SendAsync(args);

            //    // 비동기 작업이 완료될 때까지 대기합니다.
            //    if (isReceiveAsync || isSendAsync)
            //    {
            //        await Task.Factory.FromAsync(args.Completed, args.UserToken);
            //    }

            //    // 연결이 여전히 유효한지 확인합니다.
            //    return args.SocketError == SocketError.Success;
            //}
            //catch
            //{
            //    // 예외 발생 시 연결이 끊어진 것으로 간주합니다.
            //    return false;
            //}
        }
    }
}
