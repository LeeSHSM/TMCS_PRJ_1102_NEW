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

        public event EventHandler<DragEventClass> DragStarted;
        public event EventHandler<DragEventClass> DragMoved;
        public event EventHandler<DragEventClass> DragEnded;


        private MatrixFrameView _matrixFrame;
        private List<MatrixInOutSelectFrameView> _matrixInOutFrame = new List<MatrixInOutSelectFrameView>();
        
        private MatrixManager _matrixManager;
        private MatrixFrameTotalManager _matrixFrameTotalManager;

        private MatrixChannel _mappingChannel;

        private async Task CreateAsync()
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
            ChangeMatrixChannelList("INPUT");
        }

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            _matrixFrame.CellClick += MatrixFrame_CellClick;
            _matrixFrame.DragStarted += _matrixFrame_DragStarted;
            _matrixFrame.DragMoved += _matrixFrame_DragMoved;
            _matrixFrame.DragEnded += _matrixFrame_DragEnded;
        }


        #region Public Methods

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
        /// 프레임 채널리스트 변경하기
        /// </summary>
        /// <param name="channelType"></param>
        public void ChangeMatrixChannelList(string channelType)
        {
            _matrixFrame.ChannelType = MatrixFrame_ChangeMatrixChannelListClick(channelType);
        }

        /// <summary>
        /// Frame 가져오기 
        /// </summary>
        /// <returns></returns>
        public UserControl GetMatrixFrame()
        {
            UserControl uc = (UserControl)_matrixFrame;

            return uc;
        }

        /// <summary>
        /// 매트릭스 인아웃 프레임 추가 
        /// </summary>
        /// <returns></returns>
        public MatrixInOutSelectFrame AddMatrixInOutFrame()
        {
            MatrixInOutSelectFrame mc = new MatrixInOutSelectFrame();
            mc.InputClick += Mc_InputClick;
            mc.OutputClick += Mc_OutputClick;
            mc.RouteNoChange += Mc_RouteNoChangeAsync;

            _matrixInOutFrame.Add(mc);

            return mc;
        }

        public void DeleteMatrixInOutFrame(MatrixInOutSelectFrame mc)
        {
            //추가한 인아웃 프레임 삭제하는 메서드 추가
        }
        #endregion

        #region 설정관련 Public Methods

        #endregion

        #region Event Handles
        //-----------------------매트릭스 인아웃프레임 영역-----------------------

        //인아웃프레임 클릭!(아웃)
        private void Mc_OutputClick(object? sender, EventArgs e)
        {

            if (_mappingChannel == null || _mappingChannel.ChannelType != "OUTPUT" )
            {
                //MessageBox.Show("출력신호를 선택해주세요");
                return;
            }

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
                foreach (MatrixInOutSelectFrame mc in _matrixInOutFrame)
                {
                    if (mc == frame)
                    {
                        mc.MatrixChannelOutput = _mappingChannel;
                        _matrixFrame.ClearClickedCell();
                    }
                }
            }
        }

        //인아웃프레임 클릭!(인)
        private void Mc_InputClick(object? sender, EventArgs e)
        {
            if (_mappingChannel == null || _mappingChannel.ChannelType != "INPUT")
            {
                //MessageBox.Show("입력신호를 선택해주세요");
                return;
            }
            var frame = sender as MatrixInOutSelectFrameView;       
            if( frame.MatrixChannelOutput.Port == 0 )
            {
                MessageBox.Show("출력신호를 먼저 선택해주세요");
                return;
            }

            foreach (MatrixInOutSelectFrame mc in _matrixInOutFrame)
            {
                if (mc == frame)
                {
                    mc.MatrixChannelInput = _mappingChannel;
                    _matrixFrame.ClearClickedCell();
                }
            }
        }

        //인아웃프레임에서 라우트정보 변경됨
        private async void Mc_RouteNoChangeAsync(int inputNo, int outputNo)
        {
            if(await _matrixManager.GetStateAsync())
            {
                string routeChange = $"*255CI{inputNo:D2}O{outputNo:D2}!\r\n";
                _matrixManager.SendMsgAsync(routeChange);
            }
            
            //매트릭스에 바로 송신메세지 보내기
        }

        //----------------------매트릭스 프레임 영역----------------------------
        // 드래그 시작 이벤트
        private void _matrixFrame_DragStarted(object? sender, DragEventClass e)
        {
            DragStarted?.Invoke(sender, e);
        }

        //드래그 중 이벤트
        private void _matrixFrame_DragMoved(object? sender, DragEventClass e)
        {
            DragMoved?.Invoke(sender, e);
        }

        //드래그 끝 이벤트
        private void _matrixFrame_DragEnded(object? sender, DragEventClass e)
        {
            DragEnded?.Invoke(sender, e);
        }

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

        //프레임 인아웃 채널 바꿔줌
        private string MatrixFrame_ChangeMatrixChannelListClick(string channelType)
        {
            DataTable dt = CreateDataTableForMatrixChannelList(channelType);
            _matrixFrame.SetMatrixChannelList(dt);

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
