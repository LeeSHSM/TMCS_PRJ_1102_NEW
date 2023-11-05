using System;
using System.Collections.Generic;
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

        private List<MatrixChannel> _showInputChannels;
        private List<MatrixChannel> _showOutputChannels;

        public Matrix Matrix { get => _matrix; }
        public string RoomName { get => _roomName; set => _roomName = value; }
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }

        private MatrixConnectInfo _connectInfo;
        public MatrixConnectInfo ConnectInfo { get => _connectInfo; set => _connectInfo = value; }
        

        #endregion

        #region 생성자
        public MatrixManager(Matrix matrix)
        {
            _matrix = matrix;
        }

        public async Task InitializeChannels()
        {
            await Task.Run(async () =>
            {
                List<MatrixChannel> inputChannels = await LoadChannelAsync(input);
                List<MatrixChannel> outputChannels = await LoadChannelAsync(output);

                _matrix.InputChannel = inputChannels;
                _matrix.OutputChannel = outputChannels;
                _showInputChannels = inputChannels.ConvertAll(x => new MatrixChannel
                {
                    ChannelName = x.ChannelName,
                    ChannelType = x.ChannelType,
                    Port = x.Port,
                    RouteNo = x.RouteNo
                });
                _showOutputChannels = outputChannels.ConvertAll(x => new MatrixChannel
                {
                    ChannelName = x.ChannelName,
                    ChannelType = x.ChannelType,
                    Port = x.Port,
                    RouteNo = x.RouteNo
                });
            });
        }

        #endregion

        #region Event Handels

        #endregion

        #region Private Methods

        #endregion

        #region GET, SET Methods...

        public MatrixChannel GetChannelInfo(int rowNum, string channelType)
        {
            List<MatrixChannel> channels = GetChannelListInfo(channelType);
            MatrixChannel channel = new MatrixChannel
            {
                ChannelName = channels[rowNum].ChannelName,
                ChannelType = channels[rowNum].ChannelType,
                Port = channels[rowNum].Port,
                RouteNo = channels[rowNum].RouteNo
            };
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
        /// DataTable 형식으로 채널 전체정보 반환
        /// </summary>
        public DataTable GetChannelListInfoToDataTable(string channelType)
        {
            List<MatrixChannel> channels = GetChannelListInfo(channelType);

            DataTable dt = new DataTable();
            dt.Columns.Add("Port");
            dt.Columns.Add("Name");
            dt.Columns.Add("ChannelType");
            foreach (var channel in channels)
            {
                DataRow row = dt.NewRow();
                row["Port"] = channel.Port;
                row["Name"] = channel.ChannelName;
                row["ChannelType"] = channel.ChannelType;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public void SetChannel(int rowNum, string channelName, string channelType)
        {
            Debug.WriteLine(rowNum + " " + channelName);
            List<MatrixChannel> showChannels = GetChannelListInfo(channelType);
            List<MatrixChannel> originChannels = GetOriginChannelListInfo(channelType);
            showChannels[rowNum].ChannelName = channelName;

            originChannels[(showChannels[rowNum].Port)-1].ChannelName = channelType;

            SaveChannelToDB(_connectionString, showChannels[rowNum].Port, channelName, channelType);

            Debug.WriteLine("바꿈! : "+showChannels[rowNum].ChannelName);
        }

        public void SetChannelList(DataTable dataTable, string channelType)
        {
            List<MatrixChannel> matrixChannels = new List<MatrixChannel>();

            foreach (DataRow row in dataTable.Rows)
            {
                matrixChannels.Add(new MatrixChannel
                {
                    Port = int.Parse(row["Port"].ToString()),
                    ChannelName = row["Name"].ToString(),
                    ChannelType = row["ChannelType"].ToString(),
                    RouteNo = 0
                });
            }
            List<MatrixChannel> showChannels = GetChannelListInfo(channelType);

            for(int i = 0; i < showChannels.Count; i++)
            {
                if (showChannels[i].ChannelName != matrixChannels[i].ChannelName)
                {
                    Debug.WriteLine((i+1) +" 포트가 다름!");
                }
            }
        }

        #endregion

        private List<MatrixChannel> GetOriginChannelListInfo(string inout)
        {
            List<MatrixChannel> channels = new List<MatrixChannel>();
            if (inout == input)
            {
                channels = _matrix.InputChannel;
            }
            else if (inout == output)
            {
                channels = _matrix.OutputChannel;
            }
            else
            {
                return null;
            }
            return channels;
        }

        #region LoadForDB Methods...

        private async Task SaveChannelToDB(string connectionString, int port, string channelName, string channelType)
        {
            string query = "UPDATE tbl_matrix_sysinfo SET channelname = @channelname WHERE port = @port AND channeltype = @channeltype";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 데이터 유형을 명시적으로 지정하는 것이 좋습니다. 예를 들어:
                    command.Parameters.Add(new SqlParameter("@channelname", SqlDbType.VarChar)).Value = channelName;
                    command.Parameters.Add(new SqlParameter("@port", SqlDbType.Int)).Value = port;
                    command.Parameters.Add(new SqlParameter("@channeltype", SqlDbType.VarChar)).Value = channelType;

                    // 데이터를 반환하지 않으므로 ExecuteNonQueryAsync 사용
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // rowsAffected를 사용하여 결과를 처리하거나 로그에 기록할 수 있습니다.
                    Debug.WriteLine($"{rowsAffected} rows updated.");
                }

            }
        }
       
        /// <summary>
        /// 채널정보 불러오기
        /// </summary>
        /// <param name="channelType"></param>
        /// <returns></returns>
        private async Task<List<MatrixChannel>> LoadChannelAsync(string channelType)
        {
            //string connectionString = "Server=192.168.50.50;Database=TMCS;User Id=sa;password=tkdgus12#;";

            List<MatrixChannel> mc = new List<MatrixChannel>();

            for (int port = 1; port <= _matrix.getChannelPortCount(channelType); port++)
            {
                MatrixChannel channel = await GetMatrixChannelFromDBAsync(_connectionString, port, channelType);
                mc.Add(channel);
            }

            return mc;
        }
        /// <summary>
        /// DB로부터 채널정보 개별 불러오기 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="port"></param>
        /// <param name="channelType"></param>
        /// <returns></returns>
        private async Task<MatrixChannel> GetMatrixChannelFromDBAsync(string connectionString, int port, string channelType)
        {
            // 기본값 설정
            string defaultName = "불러오기실패";
            string defaultChannelType = channelType;
            int defaultRouteNo = 0;

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

        #endregion


        public void StartConnect()
        {
            _connectInfo.StartConnect();
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

    }

    public interface MatrixConnectInfo
    {
        IPAddress Address { get; set; }
        int Port { get; set; }
        void StartConnect();
        void DisConnect();
        Task< bool> GetState();
        void SendMsg(string msg);

        Task SendMsgAsync(string msg);
    }

    public class RTVDMMatrixToIP : MatrixConnectInfo
    {
        public RTVDMMatrixToIP(IPAddress ip, int port)
        {
            Address = ip;
            Port = port;
        }

        private TcpClient _client;
        private NetworkStream _stream;
        private StreamReader _reader;
        private CancellationTokenSource _cts;

        public IPAddress Address { get; set; }
        public int Port { get; set; }

        public async void StartConnect()
        {
            _client = new TcpClient();
            await _client.ConnectAsync(Address, Port);
            _stream = _client.GetStream();
            _reader = new StreamReader(_stream);
            
            
            _cts = new CancellationTokenSource();

            // Start reading in a background task
            ReceiveMessages(_cts.Token);
        }

        public void DisConnect()
        {
                _cts?.Cancel();
                _reader?.Dispose();
                _stream?.Dispose();
                _client?.Close();            
        }

        public Task< bool> GetState()
        {
            // _client가 null이 아니고, 연결되어 있으면 true를 반환합니다.
            // 그리고 _stream이 null이 아니고 소켓이 연결되어 있으면 true를 반환합니다.
            bool state = _client != null && _client.Connected && _stream != null && _stream.CanRead;
            return Task.FromResult(state);
        }

        public async Task SendMsgAsync(string msg)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(msg);

            await _stream.WriteAsync(asciiBytes, 0, asciiBytes.Length);
        }

        private async void ReceiveMessages(CancellationToken ct)
        {
            Debug.WriteLine("서버로부터 데이터 수신 대기시작!");
            //await Task.Run(async () =>
            //{
            //    try
            //    {
            //        // Keep reading the stream as long as we're connected
            //        while (!_cts.IsCancellationRequested)
            //        {
            //            string message = await _reader.ReadLineAsync();
            //            if (message != null)
            //            {
            //                // Here, you can do something with the received message
            //                Debug.WriteLine(message);
            //            }
            //        }
            //    }
            //    catch (IOException ex)
            //    {
            //        Debug.WriteLine("Error during receive: " + ex.Message);
            //        DisConnect();
            //    }
            //});

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


        public void SendMsg(string msg)
        {
            
        }
    }
}
