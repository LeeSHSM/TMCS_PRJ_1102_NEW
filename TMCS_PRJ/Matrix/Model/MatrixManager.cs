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

namespace TMCS_PRJ
{
    public class MatrixManager
    {
        #region Properties
        private string input = GlobalSetting.ChannelType.INPUT.ToString();
        private string output = GlobalSetting.ChannelType.OUTPUT.ToString();

        private Matrix _matrix;
        private string _roomName;

        public Matrix Matrix { get => _matrix; }
        public string RoomName { get => _roomName; set => _roomName = value; }


        private MatrixConnect _connect;
        public MatrixConnect Connect { get => _connect; set => _connect = value; }               

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
                List<MatrixChannel> inputChannels = await LoadChannelFromDB(input);
                List<MatrixChannel> outputChannels = await LoadChannelFromDB(output);

                _matrix.InputChannel = inputChannels;
                _matrix.OutputChannel = outputChannels;
            });

        }

        #endregion

        #region Event Handels

        #endregion

        #region Private Methods

        #endregion

        #region GET, SET Methods...

        public MatrixChannel GetChannelInfo(string inout, int channel)
        {
            List<MatrixChannel> channels = GetChannelListInfo(inout);
            return channels[channel];
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
                channels = Matrix.InputChannel;
            }
            else if (inout == output)
            {
                channels = Matrix.OutputChannel;
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
        public DataTable GetChannelListInfoToDataTable(string inout)
        {
            List<MatrixChannel> channels = GetChannelListInfo(inout);

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

        public void SetChannel(MatrixChannel channel)
        {
            List<MatrixChannel> channels = GetChannelListInfo(channel.ChannelType);
            channels[channel.Port - 1] = channel;
        }

        public void SetChannelList(string inout, List<MatrixChannel> matrixChannels)
        {
            List<MatrixChannel> channels = GetChannelListInfo(inout);

            channels = matrixChannels;
        }

        #endregion

        #region LoadForDB Methods...
       
        private async Task<List<MatrixChannel>> LoadChannelFromDB(string channelType)
        {
            string connectionString = "Server=192.168.50.50;Database=TMCS;User Id=sa;password=tkdgus12#;";

            List<MatrixChannel> mc = new List<MatrixChannel>();

            for (int port = 1; port <= 16; port++)
            {
                MatrixChannel channel = await GetMatrixChannelFromDBAsync(connectionString, port, channelType);
                mc.Add(channel);
            }

            return mc;
        }

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
                Name = defaultName,
                ChannelType = defaultChannelType,
                RouteNo = defaultRouteNo
            };
        }

        #endregion


        public void Connent()
        {
            _connect.Connect();
        }

        public void DisConnect()
        {
            _connect.DisConnect();
        }

        public void GetState(string msg)
        {
            _connect.GetState(msg);
        }

        public void SendMsg(string msg)
        {
            _connect.SendMsg(msg);
        }

    }

    public interface MatrixConnect
    {
        IPAddress Address { get; set; }
        int Port { get; set; }
        void Connect();
        void DisConnect();
        void GetState(string msg);
        void SendMsg(string msg);
    }

    public class RTVDMMatrixToIP : MatrixConnect
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

        public async void Connect()
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

        public void GetState(string msg)
        {
            throw new NotImplementedException();
        }


        public async Task SendMsg(string msg)
        {
            string myString = "*255CI02O03!\r\n";
            byte[] asciiBytes = Encoding.ASCII.GetBytes(myString);


            if (_stream == null)
                throw new InvalidOperationException("Not connected to the server.");

            // Convert the message to ASCII bytes
            byte[] bytesToSend = Encoding.ASCII.GetBytes(msg);

            // Send the message
            await _stream.WriteAsync(asciiBytes, 0, asciiBytes.Length);
        }

        private async void ReceiveMessages(CancellationToken ct)
        {
            try
            {
                // Keep reading the stream as long as we're connected
                while (!_cts.IsCancellationRequested)
                {
                    string message = await _reader.ReadLineAsync();
                    if (message != null)
                    {
                        // Here, you can do something with the received message
                        Debug.WriteLine("Received: " + message);
                    }
                }
            }
            catch (IOException ex)
            {
                Debug.WriteLine("Error during receive: " + ex.Message);
                DisConnect();
            }
        }


        void MatrixConnect.SendMsg(string msg)
        {


            SendMsg(msg);
        }
    }
}
