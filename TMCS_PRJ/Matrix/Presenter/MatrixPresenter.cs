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
        #region Properties

        private MatrixFrameView _matrixFrame;
        private List<MatrixInOutSelectFrameView> _matrixInOutFrame = new List<MatrixInOutSelectFrameView>();

        private MatrixManager _matrixManager;
        private MatrixFrameTotalManager _matrixFrameTotalManager;

        private MatrixChannel _mappingChannel;
        IProgress<ProgressReport> _progress;

        #endregion

        #region 초기화 Mewthods
        public MatrixPresenter(int inputCount, int outputCount, IProgress<ProgressReport> progress)
        {
            _progress = progress;
            _matrixFrame = new MatrixFrame();
            _matrixManager = new MatrixManager(new Matrix(inputCount, outputCount), progress);
            _matrixFrameTotalManager = new MatrixFrameTotalManager();

            InitializeEvent();
        }

        /// <summary>
        /// async 초기화
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            _progress?.Report(new ProgressReport { Message = "매트릭스매니저 초기화 시작" });
            await _matrixManager.InitializeChannels(); // MatrixManager의 초기화가 완료될 때까지 기다립니다.            
            _progress?.Report(new ProgressReport { Message = "매트릭스매니저 초기화 완료" });
            ChangeMatrixChannelList("INPUT");


            //채널설정 초기화 끝나면 최초로 폼 전달역할

        }

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            InitializeMatrixFrame();
        }

        private void InitializeMatrixFrame()
        {
            _matrixFrame.CellClick += MatrixFrame_CellClick;
            _matrixFrame.DragStarted += _matrixFrame_DragStarted;
            _matrixFrame.DragMoved += _matrixFrame_DragMoved;
            _matrixFrame.DragEnded += _matrixFrame_DragEnded;
            _matrixFrame.CellValueChanged += _matrixFrame_CellValueChanged;
        }
        #endregion

        #region Public Methods

        public void RequestDragEnded(object? sender, DragEventClass e)
        {
            //progress?.Report(new ProgressReport { Message = "리퀘스트 드래그 엔디드!!" });
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
            MatrixInOutSelectFrame MioFrame = new MatrixInOutSelectFrame();
            MioFrame.InputClick += MioFrame_InputClick;
            MioFrame.OutputClick += MioFrame_OutputClick;
            MioFrame.RouteNoChange += MioFrame_RouteNoChangeAsync;
            MioFrame.MioResizeStarted += MioFrame_MioResizeStarted;
            MioFrame.MioResizeMove += MioFrame_MioResizeMove;
            MioFrame.MioResizeFinished += MioFrame_MioResizeFinished;
            MioFrame.MioFrameDelete += MioFrame_MioFrameDelete;

            _matrixInOutFrame.Add(MioFrame);

            return MioFrame;
        }





        //-------------------------------------------- Matrix Manager 통신관련 메서드 --------------------------------------------
        public void StartConnection()
        {
            _matrixManager.StartConnectAsync();
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
            _matrixManager.SetDB(connectionString);
            _matrixFrameTotalManager.ConnectionString = connectionString;
        }

        #endregion

        #region Private Methods

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

        #region Event Handles
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

            _matrixManager.SetChannel(rowNum, channelName, channelType) ;

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

        private void MioFrame_MioFrameDelete(object? sender, EventArgs e)
        {
            MatrixInOutSelectFrame mioFrame = sender as MatrixInOutSelectFrame;
            if (_matrixInOutFrame.Contains(mioFrame)) // 리스트에 mioFrame이 실제로 있는지 확인
            {
                MioFrameDelete?.Invoke(sender, e);
                _matrixInOutFrame.Remove(mioFrame); // mioFrame 객체를 리스트에서 제거
            }            
        }

        //-------------------------------------------------매트릭스 인아웃프레임 영역------------------------------------------------

        // MioFrame 크기조절 영역

        private void MioFrame_MioResizeStarted(object? sender, MioFrameResizeEventClass e)
        {
            MioFrameResizeStarted?.Invoke(sender, e);
        }

        private void MioFrame_MioResizeMove(object? sender, MioFrameResizeEventClass e)
        {
            MioFrameResizeMoved?.Invoke(sender, e);
        }

        private void MioFrame_MioResizeFinished(object? sender, MioFrameResizeEventClass e)
        {
            MioFrameResizeEnded?.Invoke(sender, e);
        }

        // MioFrame 클릭 영역

        private void MioFrame_OutputClick(object? sender, EventArgs e)
        {
            MatrixInOutSelectFrame frame = sender as MatrixInOutSelectFrame;
            ChangeMatrixOutputInMioFrame(frame);
        }

        private void MioFrame_InputClick(object? sender, EventArgs e)
        {
            MatrixInOutSelectFrame frame = sender as MatrixInOutSelectFrame;
            ChangeMatrixInputInMioFrame(frame);
        }

        // MioFrame RouteNo 변경
        private async void MioFrame_RouteNoChangeAsync(MatrixChannel mcInput, MatrixChannel mcOutput)
        {
            await _matrixManager.UpdateRouteNoAsync(mcInput, mcOutput);
        }

        //-------------------------------------------------매트릭스 프레임 영역--------------------------------------------------
        // 드래그 시작 이벤트
        private void _matrixFrame_DragStarted(object? sender, DragEventClass e)
        {
            MFrameDragStarted?.Invoke(_mappingChannel, e);
        }

        //드래그 중 이벤트
        private void _matrixFrame_DragMoved(object? sender, DragEventClass e)
        {
            MFrameDragMoved?.Invoke(sender, e);
        }

        //드래그 끝 이벤트
        private void _matrixFrame_DragEnded(object? sender, DragEventClass e)
        {
            MFrameDragEnded?.Invoke(sender, e);
        }

        //프레임 셀 클릭
        private void MatrixFrame_CellClick(object? sender, EventArgs e)
        {
            //MatrixChannel mc = sender as MatrixChannel;
            DataGridView dgv = sender as DataGridView;
            DataGridViewCellEventArgs dgvEvent = e as DataGridViewCellEventArgs;
            if (dgv != null)
            {
                Debug.WriteLine(dgv.SelectedCells[0]);
                var cell = dgv.SelectedCells[0];

                _mappingChannel = _matrixManager.GetChannelInfo(cell.RowIndex, _matrixFrame.NowChannelType);
            }
            else if(dgv == null)
            {
                _mappingChannel = null;
            }
            _matrixFrame.SelectedChannel = _mappingChannel;
        }

        //MFrame In / Out 변경 및 채널타입 반환
        private string MatrixFrame_ChangeMatrixChannelListClick(string channelType)
        {
            DataTable dt = CreateDataTableForMatrixChannelList(channelType);
            _matrixFrame.SetMatrixChannelList(dt);

            return channelType;
        }
        #endregion

        #region Private

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
        #endregion

        public event EventHandler<DragEventClass> MFrameDragStarted;
        public event EventHandler<DragEventClass> MFrameDragMoved;
        public event EventHandler<DragEventClass> MFrameDragEnded;

        public event EventHandler<MioFrameResizeEventClass> MioFrameResizeStarted;
        public event EventHandler<MioFrameResizeEventClass> MioFrameResizeMoved;
        public event EventHandler<MioFrameResizeEventClass> MioFrameResizeEnded;

        public event EventHandler MioFrameDelete;
    }
}
