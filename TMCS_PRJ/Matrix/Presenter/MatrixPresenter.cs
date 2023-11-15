using LshDlp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMCS_PRJ
{
    public class MatrixPresenter
    {
        #region Properties
        public event EventHandler? MioFrameDelete;
        public event EventHandler? MatrixSelectedChanged;
        public event EventHandler? DragEnded;

        private MatrixFrameView _mFrame;
        private List<MatrixInOutSelectFrameView> _mioFrames = new List<MatrixInOutSelectFrameView>();

        private MatrixManager _matrixManager;
        private MatrixFrameTotalManager _matrixFrameTotalManager;

        private MatrixChannel? _mappingChannel;
        IProgress<ProgressReport> _progress;

        #endregion

        #region 초기화 Mewthods
        public MatrixPresenter(int inputCount, int outputCount, IProgress<ProgressReport> progress)
        {
            _progress = progress;
            _mFrame = new MatrixFrame();
            _matrixManager = new MatrixManager(new Matrix(inputCount, outputCount), progress);
            _matrixFrameTotalManager = new MatrixFrameTotalManager();

            InitializeEvent();            
        }

        /// <summary>
        /// 비동기 초기화
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            _progress?.Report(new ProgressReport { Message = "매트릭스매니저 초기화 시작" });
            await _matrixManager.InitializeChannels(); // MatrixManager의 초기화가 완료될 때까지 기다립니다.            
            _progress?.Report(new ProgressReport { Message = "매트릭스매니저 초기화 완료" });

            _mioFrames = await _matrixFrameTotalManager.LoadMatrixInOutFramesInfoAsync();
        }

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            _mFrame.SelectedCellChanged += MatrixFrame_SelectedCellChanged;
            _mFrame.MatrixChannelNameChanged += _matrixFrame_CellValueChanged;
            _mFrame.MFrameToObjectDragEnded += _matrixFrame_MFrameToObjectDragEnded;                
        }

        #endregion

        #region 유저컨트롤 추가,삭제 메서드...

        public UserControl InitMatrixFrame()
        {
            UserControl uc = (UserControl)_mFrame;

            return uc;
        }

        public List<MatrixInOutSelectFrameView> InitMioFrames()
        {
            foreach (MatrixInOutSelectFrame mioFrame in _mioFrames)
            {
                if (mioFrame.MatrixChannelInput.Port > 0)
                {
                    mioFrame.MatrixChannelInput = _matrixManager.GetChannel(mioFrame.MatrixChannelInput.Port - 1, mioFrame.MatrixChannelInput.ChannelType);
                }
                if (mioFrame.MatrixChannelOutput.Port > 0)
                {
                    mioFrame.MatrixChannelOutput = _matrixManager.GetChannel(mioFrame.MatrixChannelOutput.Port - 1, mioFrame.MatrixChannelOutput.ChannelType);
                }
                mioFrame.InputClick += MioFrame_Click;
                mioFrame.OutputClick += MioFrame_Click;
                mioFrame.RouteNoChange += MioFrame_RouteNoChangeAsync;
                mioFrame.MioFrameDelete += MioFrame_MioFrameDelete;
            }
            return _mioFrames;
        }

        /// <summary>
        /// 매트릭스 인아웃 프레임 추가 
        /// </summary>
        /// <returns></returns>
        public MatrixInOutSelectFrame AddMatrixInOutFrame()
        {
            MatrixInOutSelectFrame MioFrame = new MatrixInOutSelectFrame();
            MioFrame.InputClick += MioFrame_Click;
            MioFrame.OutputClick += MioFrame_Click;
            MioFrame.RouteNoChange += MioFrame_RouteNoChangeAsync;
            MioFrame.MioFrameDelete += MioFrame_MioFrameDelete;

            _mioFrames.Add(MioFrame);

            return MioFrame;
        }

        private void MioFrame_MioFrameDelete(object? sender, EventArgs e)
        {
            MatrixInOutSelectFrame mioFrame = sender as MatrixInOutSelectFrame;
            foreach (MatrixInOutSelectFrame frame in _mioFrames)
            {
                if (mioFrame == frame)
                {
                    _mioFrames.Remove(mioFrame);
                    MioFrameDelete?.Invoke(sender, e);
                    break;
                }
            }
        }

        #endregion

        #region GET, SET 메서드...
        //------------------------------------------MFrame----------------------------------------------------------

        public void SetMFrameChannelType(string channelType)
        {
            if (channelType != GlobalSetting.ChannelType.INPUT.ToString() &&
                channelType != GlobalSetting.ChannelType.OUTPUT.ToString())
            {
                throw new ArgumentException("잘못된 값입니다.(INPUT or OUTPUT 만 사용가능)", nameof(channelType));
            }
            _mFrame.ChannelType = channelType;
            DataTable dt = ConvertMatrixChannelListToDataTable(channelType);
            _mFrame.SetMatrixFrameChannelList(dt);            
        }

        //------------------------------------------MioFrame----------------------------------------------------------

        // mioFrame 아웃채널 변경
        private void SetMatrixOutputInMioFrame(MatrixInOutSelectFrame mioFrame)
        {
            if (_mappingChannel == null || _mappingChannel.ChannelType != "OUTPUT")
            {
                return;
            }

            foreach (MatrixInOutSelectFrame mc in _mioFrames)
            {
                if (_mappingChannel.Port == mc.MatrixChannelOutput.Port)
                {
                    MessageBox.Show("이미 지정하셧습니다.");
                    return;
                }
            }

            foreach (MatrixInOutSelectFrame mc in _mioFrames)
            {
                if (mc == mioFrame)
                {
                    mc.MatrixChannelOutput = _mappingChannel;
                    _mFrame.ClearClickedCell();
                }
            }
        }

        // mioFrame 인채널 변경
        private void SetMatrixInputInMioFrame(MatrixInOutSelectFrame mioFrame)
        {
            if (_mappingChannel == null || _mappingChannel.ChannelType != "INPUT")
            {
                return;
            }
            if (mioFrame.MatrixChannelOutput.Port == 0)
            {
                MessageBox.Show("출력신호를 먼저 선택해주세요");
                return;
            }

            foreach (MatrixInOutSelectFrame mc in _mioFrames)
            {
                if (mc == mioFrame)
                {
                    mc.MatrixChannelInput = _mappingChannel;
                    _mFrame.ClearClickedCell();
                }
            }
        }

        //------------------------------------------MatrixManager----------------------------------------------------------

        public void SaveMatrixInfo()
        {
            _matrixFrameTotalManager.SaveMatrixInOutFramesInfo(_mioFrames);
        }

        public async Task SetMatrixRouteAsync(int outPort, MatrixChannel mc )
        {
            await _matrixManager.SetRouteNoAsync(outPort, mc);
        }




        //-------------------------------------------- Matrix Manager 통신 메서드 --------------------------------------------

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
        /// MatrixManager 통신역할 인터페이스 할당 
        /// </summary>
        /// <param name="matrixConnectInfo"></param>
        public void SetConnectInfo(MatrixConnectInfo matrixConnectInfo)
        {
            _matrixManager.ConnectInfo = matrixConnectInfo;
        }

        public async Task StartConnectionAsync()
        {
            await _matrixManager.StartConnectAsync();
        }

        #endregion

        #region Event Handles

        //-------------------------------------------------매트릭스 인아웃프레임 영역------------------------------------------------


        private void MioFrame_Click(object? sender, EventArgs e)
        {
            MatrixInOutSelectFrame MioFrame = sender as MatrixInOutSelectFrame;
            if(_mappingChannel == null)
            {
                return;
            }
            if(_mappingChannel.ChannelType == "INPUT")
            {
                SetMatrixInputInMioFrame(MioFrame);
            }
            else if(_mappingChannel.ChannelType == "OUTPUT")
            {
                SetMatrixOutputInMioFrame(MioFrame);
            }            
        }


        // MioFrame RouteNo 변경
        private async void MioFrame_RouteNoChangeAsync(MatrixChannel mcInput, MatrixChannel mcOutput)
        {
            await _matrixManager.SetRouteNoAsync(mcInput, mcOutput);
        }



        //-------------------------------------------------매트릭스 프레임 영역--------------------------------------------------

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
            string channelType = _mFrame.ChannelType;

            _matrixManager.SetChannelName(rowNum, channelName, channelType);
        }

        private void _matrixFrame_MFrameToObjectDragEnded(object? sender, EventArgs e)
        {
            Form mainForm = _mFrame.GetFindForm();
            Point formCoordinates = mainForm.PointToClient(Cursor.Position);
            int width = 30;
            int height = 30;
            Rectangle mouseRect = new Rectangle(formCoordinates.X - (width), formCoordinates.Y - (height / 2), width, height);

            MatrixInOutSelectFrame largestIntersectingFrame = null;
            int largestArea = 0;

            foreach (MatrixInOutSelectFrame mioFrame in _mioFrames)
            {
                Point mioFramePosition = mioFrame.GetPositionInForm();
                Size mioFrameSize = mioFrame.Size;
                Rectangle mioFrameRect = new Rectangle(mioFramePosition, mioFrameSize);

                Rectangle intersection = Rectangle.Intersect(mouseRect, mioFrameRect);

                // 겹치는 영역의 크기 계산
                int area = intersection.Width * intersection.Height;
                if (area > largestArea)
                {
                    largestArea = area;
                    largestIntersectingFrame = mioFrame;
                }
            }

            if (largestIntersectingFrame != null)
            {
                if(_mappingChannel.ChannelType == "INPUT")
                {
                    SetMatrixInputInMioFrame(largestIntersectingFrame);
                }
                else if(_mappingChannel.ChannelType == "OUTPUT")
                {
                    SetMatrixOutputInMioFrame(largestIntersectingFrame);
                }
            }
            else
            {
                DragEnded?.Invoke(sender, e);
            }
        }

        //프레임 셀 클릭
        private void MatrixFrame_SelectedCellChanged(object? sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
            {
                var cell = dgv.SelectedCells[0];

                _mappingChannel = _matrixManager.GetChannel(cell.RowIndex, _mFrame.ChannelType);
            }
            else if(dgv == null)
            {
                _mappingChannel = null;
            }
            MatrixSelectedChanged?.Invoke(_mappingChannel, EventArgs.Empty);
        }

        #endregion




        /// <summary>
        /// Manager로부터 받아온 파일을 View에 뿌려주기위한 Methods 
        /// </summary>
        /// <param name="channelType"></param>
        /// <returns></returns>
        private DataTable ConvertMatrixChannelListToDataTable(string channelType)
        {
            DataTable dt = new DataTable();
            List<MatrixChannel> channels = _matrixManager.GetChannelList(channelType);

            string col1 = "  구  분";
            string col2 = "     소  스";

            dt.Columns.Add(col1);
            dt.Columns.Add(col2);

            foreach(MatrixChannel channel in channels)
            {
                DataRow dr = dt.NewRow();
                dr[col1] = $"{channel.ChannelType} {channel.Port}";
                dr[col2] = $"{channel.ChannelName}";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        
    }
}
