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
            _matrixFrame.CellValueChange += _matrixFrame_CellValueChange1;
            _matrixFrame.CellValueChanged += _matrixFrame_CellValueChanged;
        }

        /// <summary>
        /// 매트릭스 프레임에서 이름이 변경됨!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _matrixFrame_CellValueChanged(object? sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            DataGridViewCellEventArgs dgvEvent = e as DataGridViewCellEventArgs;
            int rowNum = dgvEvent.RowIndex;
            string channelName = dgv.Rows[dgvEvent.RowIndex].Cells[1].Value.ToString();
            string channelType = _matrixFrame.NowChannelType;

            _matrixManager.SetChannel(rowNum, channelName , channelType);

            MatrixChannel channel = _matrixManager.GetChannelInfo(rowNum, channelType);

            foreach(MatrixInOutSelectFrame item in _matrixInOutFrame)
            {
                if(channelType == "INPUT")
                {
                    if(channel.Port == item.MatrixChannelInput.Port)
                    {
                        item.MatrixChannelInput = channel;
                    }
                    
                }
                else if(channelType == "OUTPUT")
                {
                    if (channel.Port == item.MatrixChannelOutput.Port)
                    {
                        item.MatrixChannelOutput = channel;
                    }
                }

            }
        }

        private void _matrixFrame_CellValueChange1(int rowNum, string channelName)
        {
            //_matrixManager.SetChannel(rowNum, channelName, _matrixFrame.NowChannelType);
        }

        private void _matrixFrame_CellValueChange(object? sender, EventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;

            //DataTable dgvDt = (DataTable)dgv.DataSource;

            //DataTable dt = new DataTable();
            //dt.Columns.Add("Port");
            //dt.Columns.Add("Name");
            //dt.Columns.Add("ChannelType");

            //foreach (DataRow dr in dgvDt.Rows)
            //{
            //    string[] strDr = dr[0].ToString().Split(' ');
            //    DataRow dtdr = dt.NewRow();
            //    dtdr["Port"] = strDr[1];
            //    dtdr["Name"] = dr[1].ToString();
            //    dtdr["ChannelType"] = strDr[0];
            //    dt.Rows.Add(dtdr);
            //}
            //_matrixManager.SetChannelList(dt, _matrixFrame.NowChannelType);
        }


        #region Public Methods
        public void RequestDragEnded(object? sender, DragEventClass e)
        {
            MatrixInOutSelectFrame frame = sender as MatrixInOutSelectFrame;
            if (_mappingChannel.ChannelType == "INPUT")
            {
                ChangeMatrixInputInMioFrame(frame);
            }
            else if (_mappingChannel.ChannelType == "OUTPUT")
            {
                ChangeMatrixOutputInMioFrame(frame);
            }
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
        /// 프레임 채널리스트 변경하기
        /// </summary>
        /// <param name="channelType"></param>
        public void ChangeMatrixChannelList(string channelType)
        {
            _matrixFrame.NowChannelType = MatrixFrame_ChangeMatrixChannelListClick(channelType);
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
            MatrixInOutSelectFrame frame = sender as MatrixInOutSelectFrame;
            ChangeMatrixOutputInMioFrame(frame);
        }

        //인아웃프레임 클릭!(인)
        private void Mc_InputClick(object? sender, EventArgs e)
        {
            MatrixInOutSelectFrame frame = sender as MatrixInOutSelectFrame;
            ChangeMatrixInputInMioFrame(frame);
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
            DragStarted?.Invoke(_mappingChannel, e);
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

        #region Private
        // mioFrame 아웃채널 변경
        private void ChangeMatrixOutputInMioFrame(MatrixInOutSelectFrame mioFrame)
        {

            if (_mappingChannel == null || _mappingChannel.ChannelType != "OUTPUT")
            {
                //MessageBox.Show("출력신호를 선택해주세요");
                return;
            }

            foreach (MatrixInOutSelectFrame mc in _matrixInOutFrame)
            {
                if (_mappingChannel.Port == mc.MatrixChannelOutput.Port)
                {
                    MessageBox.Show("이미 지정하셧습니다.");
                    //check = true;
                    return;
                }
            }


            foreach (MatrixInOutSelectFrame mc in _matrixInOutFrame)
            {
                if (mc == mioFrame)
                {
                    mc.MatrixChannelOutput = _mappingChannel;
                    _matrixFrame.ClearClickedCell();
                }
            }

        }
        // mioFrame 인채널 변경
        private void ChangeMatrixInputInMioFrame(MatrixInOutSelectFrame mioFrame)
        {
            if (_mappingChannel == null || _mappingChannel.ChannelType != "INPUT")
            {
                //MessageBox.Show("입력신호를 선택해주세요");
                return;
            }
            if (mioFrame.MatrixChannelOutput.Port == 0)
            {
                MessageBox.Show("출력신호를 먼저 선택해주세요");
                return;
            }

            foreach (MatrixInOutSelectFrame mc in _matrixInOutFrame)
            {
                if (mc == mioFrame)
                {
                    mc.MatrixChannelInput = _mappingChannel;
                    _matrixFrame.ClearClickedCell();
                }
            }
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
