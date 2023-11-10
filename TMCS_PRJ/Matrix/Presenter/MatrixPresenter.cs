﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        }

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            _matrixFrame.SelectedCellChanged += MatrixFrame_SelectedCellChanged;
            _matrixFrame.CellValueChanged += _matrixFrame_CellValueChanged;
            _matrixFrame.MFrameToObjectDragEnded += _matrixFrame_MFrameToObjectDragEnded;

            
        }

        #endregion

        #region Public Methods

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
            //MioFrame.RouteNoChange += MioFrame_RouteNoChangeAsync;
            //MioFrame.MioResizeStarted += MioFrame_MioResizeStarted;
            //MioFrame.MioResizeMove += MioFrame_MioResizeMove;
            //MioFrame.MioResizeFinished += MioFrame_MioResizeFinished;
            //MioFrame.MioFrameDelete += MioFrame_MioFrameDelete;
            MioFrame.MatrixChannelInput.MatrixChannelValueChanged += MatrixChannelInput_MatrixChannelValueChanged;

            _matrixInOutFrame.Add(MioFrame);

            return MioFrame;
        }

        private void MatrixChannelInput_MatrixChannelValueChanged(object sender)
        {
            Debug.WriteLine("테스트");
        }

        /// <summary>
        /// 프레임 채널리스트 변경하기
        /// </summary>
        /// <param name="channelType"></param>
        public void ChangeMatrixChannelList(string channelType)
        {
            if(channelType != GlobalSetting.ChannelType.INPUT.ToString() &&
                channelType != GlobalSetting.ChannelType.OUTPUT.ToString()) 
            {
                throw new ArgumentException("잘못된 값입니다.(INPUT or OUTPUT 만 사용가능)", nameof(channelType));
            }
            _matrixFrame.NowChannelType = GetChangedMatirxListInMframe(channelType);
        }


        //-------------------------------------------- Matrix Manager 통신관련 메서드 --------------------------------------------
        public async Task StartConnectionAsync()
        {
            await _matrixManager.StartConnectAsync();
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

            _matrixManager.UpdateChannelName(rowNum, channelName, channelType) ;

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

        private void _matrixFrame_MFrameToObjectDragEnded(object? sender, EventArgs e)
        {

        }


        //프레임 셀 클릭
        private void MatrixFrame_SelectedCellChanged(object? sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
            {
                var cell = dgv.SelectedCells[0];

                _mappingChannel = _matrixManager.GetChannelInfo(cell.RowIndex, _matrixFrame.NowChannelType);
            }
            else if(dgv == null)
            {
                _mappingChannel = null;
            }
            _matrixFrame.SelectedChannel = _mappingChannel;
        }

        private void test()
        {
            //마우스 포인트를 기준으로 충돌체크
            //Point mousePositionOnForm = new Point(e.Location.X, e.Location.Y);

            //Control maxOverlapControl = null; // To keep track of the control with maximum overlap.
            //int maxOverlapArea = 0; // To keep track of the maximum overlap area.

            //foreach (Control con in _view.pnMatrixInOutSelectFrame.Controls)
            //{
            //    Point conLocation = ConvertToFormCoordinates(con.Parent, con);
            //    Rectangle conBounds = new Rectangle(conLocation, con.Size);
            //    Rectangle mouseBounds = new Rectangle(mousePositionOnForm, new Size(1, 1)); // Mouse position as a tiny rectangle.

            //    if (mouseBounds.IntersectsWith(conBounds))
            //    {
            //        // Calculate the area of intersection
            //        Rectangle intersection = Rectangle.Intersect(mouseBounds, conBounds);
            //        int overlapArea = intersection.Width * intersection.Height;

            //        // Check if this control has the maximum overlap so far
            //        if (overlapArea > maxOverlapArea)
            //        {
            //            maxOverlapArea = overlapArea;
            //            maxOverlapControl = con;
            //        }
            //    }
            //}
        }

        //MFrame In / Out 변경 및 채널타입 반환


        #endregion

        #region Private

        private string GetChangedMatirxListInMframe(string channelType)
        {
            DataTable dt = GetMatrixChannelListToDataTable(channelType);
            _matrixFrame.SetMatrixChannelList(dt);

            return channelType;
        }

        /// <summary>
        /// Manager로부터 받아온 파일을 View에 뿌려주기위한 Methods 
        /// </summary>
        /// <param name="channelType"></param>
        /// <returns></returns>
        private DataTable GetMatrixChannelListToDataTable(string channelType)
        {
            DataTable dt = new DataTable();
            List<MatrixChannel> channels = _matrixManager.GetChannelListInfo(channelType);

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
        #endregion


        public event EventHandler<MioFrameResizeEventClass> MioFrameResizeStarted;
        public event EventHandler<MioFrameResizeEventClass> MioFrameResizeMoved;
        public event EventHandler<MioFrameResizeEventClass> MioFrameResizeEnded;

        public event EventHandler MioFrameDelete;
    }
}
