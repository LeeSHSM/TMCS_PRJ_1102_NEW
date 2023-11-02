using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
            if (inout == "INPUT")
            {
                channels = Matrix.InputChannel;
            }
            else if (inout == "OUTPUT")
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
        public RTVDMMatrixToIP()
        {
        }
        public IPAddress Address { get; set; }
        public int Port { get; set; }

        public void Connect()
        {
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
        }

        public void GetState(string msg)
        {
            throw new NotImplementedException();
        }

        public void SendMsg(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
