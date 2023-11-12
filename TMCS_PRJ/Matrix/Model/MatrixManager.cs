using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static TMCS_PRJ.GlobalSetting;

namespace TMCS_PRJ
{
    public class MatrixManager
    {
        #region Properties
        private string input = GlobalSetting.ChannelType.INPUT.ToString();
        private string output = GlobalSetting.ChannelType.OUTPUT.ToString();

        private Matrix _matrix;
        private string _roomName;
        private string _connectionString;
        private MatrixConnectInfo _connectInfo;
        IProgress<ProgressReport> _progress;

        private List<MatrixChannel> _showInputChannels;
        private List<MatrixChannel> _showOutputChannels;

        public Matrix Matrix { get => _matrix; }
        public string RoomName { get => _roomName; set => _roomName = value; }
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }
        public MatrixConnectInfo ConnectInfo { get => _connectInfo; set => _connectInfo = value; }

        #endregion

        #region 초기화 Methods 
        public MatrixManager(Matrix matrix, IProgress<ProgressReport> progress)
        {
            _progress = progress;
            _matrix = matrix;
        }


        //초기화 메서드이나... Async 타입으로 MatrixManager을 외부에서 생성할때 같이 실행해줘야함.
        public async Task InitializeChannels()
        {
            await Task.Run(async () =>
            {
                _progress?.Report(new ProgressReport { Message = "인풋채널 초기화 시작" });
                List<MatrixChannel> inputChannels = await LoadChannelAsync(input);
                _progress?.Report(new ProgressReport { Message = "인풋채널 초기화 완료" });
                _progress?.Report(new ProgressReport { Message = "아웃채널 초기화 시작" });
                List<MatrixChannel> outputChannels = await LoadChannelAsync(output);
                _progress?.Report(new ProgressReport { Message = "아웃채널 초기화 완료" });

                _matrix.InputChannel = inputChannels;
                _matrix.OutputChannel = outputChannels;
                _showInputChannels = inputChannels;
                _showOutputChannels = outputChannels;

                foreach(var channel in inputChannels)
                {
                    channel.MatrixChannelValueChanged += Channel_MatrixChannelValueChanged;
                }

                foreach(var channel in outputChannels)
                {
                    channel.MatrixChannelValueChanged += Channel_MatrixChannelValueChanged;
                }
            });
        }

        private async void Channel_MatrixChannelValueChanged(object sender)
        {
            MatrixChannel mc = (MatrixChannel)sender;
            await SaveChannelToDBAsync(_connectionString,mc);
            Debug.WriteLine("변경됨!!"+mc.ChannelName);
        }

        #endregion


        #region Public Methods...

        public MatrixChannel GetChannelInfo(int rowNum, string channelType)
        {
            List<MatrixChannel> channels = GetChannelListInfo(channelType);
            MatrixChannel channel = channels[rowNum];
            return channel;
        }

        /// <summary>
        /// 리스트 형식으로 매트릭스의 채널정보 반환
        /// </summary>
        /// <param name="inout"></param>
        /// <returns></returns>
        public List<MatrixChannel> GetChannelListInfo(string inout)
        {
            List<MatrixChannel> channels = new List<MatrixChannel>();
            if (inout == input)
            {
                channels = _showInputChannels;
            }
            else if (inout == output)
            {
                channels = _showOutputChannels;
            }
            else
            {
                return null;
            }
            return channels;
        }


        /// <summary>
        /// 채널이름바꾸는 메서드
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="channelName"></param>
        /// <param name="channelType"></param>
        public async Task SetChannelNameAsync(int rowNum, string channelName, string channelType)
        {
            List<MatrixChannel> showChannels = GetChannelListInfo(channelType);
            showChannels[rowNum].ChannelName = channelName;

            //SaveChannelToDB(_connectionString, showChannels[rowNum].Port, channelName, channelType);
        }

        public async Task SetRouteNoAsync(MatrixChannel mcInput, MatrixChannel mcOutput)
        {
            if (await GetStateAsync())
            {
                mcOutput.RouteNo = mcInput.Port;
                await ChangeRouteNoToMatrixAsync(mcOutput.RouteNo, mcOutput.Port);
            }
        }
        #endregion

        #region 통신관련 Methods 

        public void SetDB(string msg1)
        {
            _connectionString = msg1;
        }

        public async Task StartConnectAsync()
        {
            await _connectInfo.StartConnectAsync();
        }

        public void DisConnect()
        {
            _connectInfo.DisConnect();
        }

        public Task<bool> GetStateAsync()
        {
            return _connectInfo.GetState();
        }

        public async Task SendMsgAsync(string msg)
        {
            await _connectInfo.SendMsgAsync(msg);
        }

        private async Task ChangeRouteNoToMatrixAsync(int inputPort, int OutputPort)
        {
            await _connectInfo.ChangeRouteNoToMatrixAsync(inputPort, OutputPort);
        }

        #endregion


        #region Private Methods



        

        /// <summary>
        /// 채널정보 불러오기
        /// </summary>
        /// <param name="channelType"></param>
        /// <returns></returns>
        private async Task<List<MatrixChannel>> LoadChannelAsync(string channelType)
        {
            int connectChecked = 0;
            while (_connectionString == null && connectChecked <= 50)
            {
                GlobalSetting.Logger.LogInfo("Conmnect 설정까지 대기중...");
                await Task.Delay(100); // 여기서는 10ms 대기, 필요에 따라 조정 가능
                connectChecked++;
            }

            if(_connectionString == null)
            {                
                return null;
            }

            List<MatrixChannel> mc = new List<MatrixChannel>();

            for (int port = 1; port <= _matrix.getChannelPortCount(channelType); port++)
            {
                MatrixChannel channel = await GetMatrixChannelInDBAsync(_connectionString, port, channelType);
                //channel.MatrixChannelValueChanged += Channel_MatrixChannelValueChanged;
                mc.Add(channel);
            }
            

            return mc;
        }

        //--------------------------------------테스트중인 메서드----------------------------------


        /// <summary>
        /// DB로부터 채널정보 개별 불러오기 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="port"></param>
        /// <param name="channelType"></param>
        /// <returns></returns>
        private async Task<MatrixChannel> GetMatrixChannelInDBAsync(string connectionString, int port, string channelType)
        {
            // 기본값 설정
            string defaultName = "불러오기실패";
            string defaultChannelType = channelType;
            int defaultRouteNo = 0;

            //일단 소스에 쿼리문 박고.. 추후에 EF Core 공부해서 수정하자
            string query = $"SELECT channelname, channeltype, routeno FROM tbl_matrix_sysinfo WHERE port = @port and channeltype = @channeltype";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@port", port);
                    command.Parameters.AddWithValue("@channeltype", channelType);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // 데이터가 있다면
                        {
                            return new MatrixChannel
                            {
                                Port = port,
                                ChannelName = reader.GetString(0), // 첫 번째 컬럼 (name)
                                ChannelType = reader.GetString(1), // 두 번째 컬럼 (channeltype)
                                RouteNo = reader.GetInt32(2) // 세 번째 컬럼 (routeno)                                
                            };
                        }
                    }
                }
            }
            return new MatrixChannel
            {
                Port = port,
                ChannelName = defaultName,
                ChannelType = defaultChannelType,
                RouteNo = defaultRouteNo
            };
        }

        private async Task SaveChannelToDB(string connectionString, int port, string channelName, string channelType)
        {
            //일단 소스에 쿼리문 박고.. 추후에 EF Core 공부해서 수정하자
            string query = "UPDATE tbl_matrix_sysinfo SET channelname = @channelname, routeno = @routeno WHERE port = @port AND channeltype = @channeltype";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // DB저장... 추후에 EF Core로 수정
                    command.Parameters.Add(new SqlParameter("@channelname", SqlDbType.VarChar)).Value = channelName;
                    command.Parameters.Add(new SqlParameter("@port", SqlDbType.Int)).Value = port;
                    command.Parameters.Add(new SqlParameter("@channeltype", SqlDbType.VarChar)).Value = channelType;
                    command.Parameters.Add(new SqlParameter("@routeno", SqlDbType.Int)).Value = 0;

                    // 데이터를 반환하지 않으므로 ExecuteNonQueryAsync 사용
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // rowsAffected를 사용하여 결과를 처리 추후 로그에 저장
                    Debug.WriteLine($"{rowsAffected} rows updated.");
                }
            }
        }
        private async Task SaveChannelToDBAsync(string connectionString, MatrixChannel mc)
        {
            //일단 소스에 쿼리문 박고.. 추후에 EF Core 공부해서 수정하자
            string query = "UPDATE tbl_matrix_sysinfo SET channelname = @channelname, routeno = @routeno WHERE port = @port AND channeltype = @channeltype";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // DB저장... 추후에 EF Core로 수정
                    command.Parameters.Add(new SqlParameter("@channelname", SqlDbType.VarChar)).Value = mc.ChannelName;
                    command.Parameters.Add(new SqlParameter("@port", SqlDbType.Int)).Value = mc.Port;
                    command.Parameters.Add(new SqlParameter("@channeltype", SqlDbType.VarChar)).Value = mc.ChannelType;
                    command.Parameters.Add(new SqlParameter("@routeno", SqlDbType.Int)).Value = mc.RouteNo;

                    // 데이터를 반환하지 않으므로 ExecuteNonQueryAsync 사용
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // rowsAffected를 사용하여 결과를 처리 추후 로그에 저장
                    Debug.WriteLine($"{rowsAffected} rows updated.");
                }
            }
        }

        #endregion




    }
    // 매트릭스 통신관련 인터페이스.... 추후 추가될수도 있음
    public interface MatrixConnectInfo
    {
        IPAddress Address { get; set; }
        int Port { get; set; }

        Task StartConnectAsync();

        Task DisConnect();

        Task<bool> GetState();

        Task SendMsgAsync(string msg);

        Task ChangeRouteNoToMatrixAsync(int inputPort, int OutputPort);
    }

    public class RTVDMMatrixToIP : MatrixConnectInfo
    {
        IProgress<ProgressReport> _progress;
        public RTVDMMatrixToIP(IPAddress ip, int port, IProgress<ProgressReport> progress)
        {
            _progress = progress;
            Address = ip;
            Port = port;
        }

        private TcpClient _client;
        private NetworkStream _stream;
        private StreamReader _reader;
        private CancellationTokenSource _cts;

        public IPAddress Address { get; set; }
        public int Port { get; set; }

        public async Task StartConnectAsync()
        {
            _client = new TcpClient();            
           
            int retryCount = 0;
            int maxRetryCount = 1;
            while (retryCount < maxRetryCount)
            {
                if (!(await GetState()))
                {
                    try
                    {
                        _progress?.Report(new ProgressReport { Message = $"매트릭스 서버 접속중..." });
                        await _client.ConnectAsync(Address, Port);
                        break;
                    }                 
                    catch
                    {
                        retryCount++;
                        _progress?.Report(new ProgressReport { Message = $"재연결 시도 {retryCount}/{maxRetryCount}" });
                        // 연결 실패: 반복 횟수 증가
                        
                        Debug.WriteLine($"재연결 시도 {retryCount}/{maxRetryCount}");

                    }
                }
                else
                {
                    break;
                }
            }
            _stream = _client.GetStream();
            _reader = new StreamReader(_stream);

            _cts = new CancellationTokenSource();
            ReceiveMessages(_cts.Token);
        }

        public async Task DisConnect()
        {
            _cts?.Cancel();
            _reader?.Dispose();
            _stream?.Dispose();
            _client?.Close();
        }

        public Task<bool> GetState()
        {
            // _client가 null이 아니고, 연결되어 있으면 true를 반환합니다.
            // 그리고 _stream이 null이 아니고 소켓이 연결되어 있으면 true를 반환합니다.
            bool state = _client != null && _client.Connected && _stream != null && _stream.CanRead;
            return Task.FromResult(state);
        }

        public async Task SendMsgAsync(string msg)
        {
            const int maxRetryCount = 5;
            int retryCount = 0;

            while (retryCount < maxRetryCount)
            {
                if (!await GetState())
                {
                    await DisConnect();

                    try
                    {
                        await StartConnectAsync();
                        break; // 연결 성공하면 반복 중단
                    }
                    catch
                    {
                        // 연결 실패: 반복 횟수 증가
                        retryCount++;
                        GlobalSetting.Logger.LogError($"재연결 시도 {retryCount}/{maxRetryCount}");
                        // 재연결을 시도하기 전에 짧은 지연을 둘 수 있습니다.
                        await Task.Delay(1000); // 1초간 대기
                    }
                }
                else
                {                    
                    break;
                }
            }

            try            
            {
                // 연결된 상태에서 메시지 전송
                byte[] asciiBytes = Encoding.ASCII.GetBytes(msg);
                await _stream.WriteAsync(asciiBytes, 0, asciiBytes.Length);
            }
            catch (Exception ex) 
            {
                GlobalSetting.Logger.LogError("서버에 연결할 수 없습니다. 메시지를 전송하지 못했습니다." + ex);
            }
        }

        private async Task ReceiveMessages(CancellationToken ct)
        {
            Debug.WriteLine("서버로부터 데이터 수신 대기시작!");
            try
            {
                string message = await _reader.ReadLineAsync();
                Debug.WriteLine(message);
                //버퍼와 문자열 빌더를 설정합니다.
                byte[] buffer = new byte[1024];
                StringBuilder stringBuilder = new StringBuilder();

                //연결이 유지되는 동안 계속 수신합니다.
                while (!_cts.IsCancellationRequested)
                {
                    int numberOfBytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, ct);
                    string receivedText = Encoding.ASCII.GetString(buffer, 0, numberOfBytesRead);

                    //메시지를 문자열 빌더에 추가합니다.
                    stringBuilder.Append(receivedText);

                    //'!' 문자가 있는지 확인하고, 있다면 메시지를 처리합니다.
                    int messageEndIndex;
                    while ((messageEndIndex = stringBuilder.ToString().IndexOf('!')) != -1)
                    {
                        //메시지의 시작부터 '!' 문자가 있는 부분까지를 추출합니다.
                        string completeMessage = stringBuilder.ToString(0, messageEndIndex + 1);

                        completeMessage = completeMessage.TrimStart('\r', '\n');
                        //MessageBox.Show(stringBuilder.ToString());
                        Debug.WriteLine(completeMessage);

                        //추출된 메시지를 빌더에서 제거합니다.
                        stringBuilder.Remove(0, messageEndIndex + 1);
                    }
                }
            }
            catch (IOException ex)
            {
                Debug.WriteLine("Error during receive: " + ex.Message);
                //오류 발생 시 연결을 종료합니다.
                DisConnect();
            }
            catch (OperationCanceledException)
            {
                //취소 요청이 들어올 경우 처리합니다.
                Debug.WriteLine("Receiving cancelled.");
            }
        }

        public async Task ChangeRouteNoToMatrixAsync(int inputPort, int OutputPort)
        {
            string routeChange = $"*255CI{inputPort:D2}O{OutputPort:D2}!\r\n";
            Debug.Write("서버로 전송 : " + routeChange);
            SendMsgAsync(routeChange);
        }
    }
}
