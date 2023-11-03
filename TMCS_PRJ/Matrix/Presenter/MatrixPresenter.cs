using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class MatrixPresenter
    {
        public MatrixPresenter(int inputCount, int outputCount)
        {
            _matrixFrame = new MatrixFrame();
            _matrixManager = new MatrixManager(new Matrix(inputCount, outputCount));
            _matrixFrameTotalManager = new MatrixFrameTotalManager();
            CreateAsync();
        }

        private MatrixFrameView _matrixFrame;
        private List<MatrixInOutSelectFrameView> _matrixInOutFrame = new List<MatrixInOutSelectFrameView>();
        
        private MatrixManager _matrixManager;
        private MatrixFrameTotalManager _matrixFrameTotalManager;

        private MatrixChannel _mappingChannel;

        public MatrixFrameView MatrixFrameControl { get => _matrixFrame;}

        public async Task CreateAsync()
        {
            await InitializeAsync();
        }

        /// <summary>
        /// async 초기화
        /// </summary>
        /// <returns></returns>
        private async Task InitializeAsync()
        {
            await _matrixManager.InitializeChannels(); // MatrixManager의 초기화가 완료될 때까지 기다립니다.
            InitializeEvent();

            //채널설정 초기화 끝나면 최초로 폼 전달역할
            MatrixFrameControl.ChannelType = MatrixFrame_ChangeMatrixChannelListClick("INPUT");
        }

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            MatrixFrameControl.ChangeMatrixChannelListClick += MatrixFrame_ChangeMatrixChannelListClick;
            MatrixFrameControl.CellClick += MatrixFrame_CellClick;
        }

        /// <summary>
        /// MatrixManager 통신역할 인터페이스 할당 
        /// </summary>
        /// <param name="matrixConnectInfo"></param>
        public void SetConnectInfo(MatrixConnectInfo matrixConnectInfo)
        {
            _matrixManager.ConnectInfo = matrixConnectInfo;
        }

        /// <summary>
        /// DB접속정보 할당 
        /// </summary>
        /// <param name="connectionString"></param>
        public void SetConnectDBInfo(string connectionString)
        {
            _matrixManager.ConnectionString = connectionString;
            _matrixFrameTotalManager.ConnectionString = connectionString;
        }

        /// <summary>
        /// 매트릭스 인아웃 프레임 추가 
        /// </summary>
        /// <returns></returns>
        public MatrixInOutSelectFrame AddMatrixInOutFrame()
        {
            MatrixInOutSelectFrame mc = new MatrixInOutSelectFrame();
            mc.InputClick += Mc_matrixChannelInputClick;
            mc.OutputClick += Mc_matrixChannelOutputClick;

            _matrixInOutFrame.Add(mc);

            return mc;
        }

        public void DeleteMatrixInOutFrame(MatrixInOutSelectFrame mc)
        {

        }

        #region 설정관련 Public Methods

        #endregion

        #region Event Handles
        //-----------------------매트릭스 인아웃프레임 영역-----------------------

        //인아웃프레임 클릭!(아웃)
        private void Mc_matrixChannelOutputClick(object? sender, EventArgs e)
        {
            var frame = sender as MatrixInOutSelectFrameView;
            bool check = false;
            foreach (MatrixInOutSelectFrameView mc in _matrixInOutFrame)
            {
                if (_mappingChannel.Port == mc.MatrixChannelOutput.Port)
                {
                    MessageBox.Show("이미 지정하셧습니다.");
                    check = true;
                    return;
                }
            }

            if (!check)
            {
                foreach (MatrixInOutSelectFrameView mc in _matrixInOutFrame)
                {
                    if (mc == frame)
                    {
                        mc.MatrixChannelOutput = frame.MatrixChannelOutput;
                        frame.MatrixChannelOutput = _mappingChannel;
                    }
                }
            }
        }

        //인아웃프레임 클릭!(인)
        private void Mc_matrixChannelInputClick(object? sender, EventArgs e)
        {
            var frame = sender as MatrixInOutSelectFrameView;            

            foreach (MatrixInOutSelectFrameView mc in _matrixInOutFrame)
            {
                if (mc == frame)
                {
                    mc.MatrixChannelInput = frame.MatrixChannelInput;
                    frame.MatrixChannelInput = _mappingChannel;
                }
            }

        }
        //----------------------매트릭스 프레임 영역----------------------------
        //프레임 셀 클릭
        private void MatrixFrame_CellClick(object? sender, EventArgs e)
        {
            MatrixChannel mc = sender as MatrixChannel;
            _mappingChannel = mc;
            if (mc != null)
            {
                Debug.WriteLine(mc.ChannelName);
            }
            else
            {
                Debug.WriteLine("mc == null");
            }
        }

        private string MatrixFrame_ChangeMatrixChannelListClick(string channelType)
        {
            DataTable dt = CreateDataTableForMatrixChannelList(channelType);
            MatrixFrameControl.SetMatrixChannelList(dt);

            return channelType;

        }
        #endregion



        /// <summary>
        /// Manager로부터 받아온 파일을 View에 뿌려주기위한 Methods 
        /// </summary>
        /// <param name="channelType"></param>
        /// <returns></returns>
        private DataTable CreateDataTableForMatrixChannelList(string channelType)
        {
            DataTable dt = new DataTable();
            DataTable List = _matrixManager.GetChannelListInfoToDataTable(channelType);
            dt.Columns.Add("  구  분");
            dt.Columns.Add("     소  스");

            foreach (DataRow row in List.Rows)
            {
                DataRow dr = dt.NewRow();
                string strTmp = row["ChannelType"].ToString() + " " + row["Port"];
                dr["  구  분"] = strTmp;
                dr["     소  스"] = row["Name"];
                dt.Rows.Add(dr);
            }
            return dt;
        }

        
    }
}
