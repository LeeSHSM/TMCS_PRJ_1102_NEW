using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMCS_PRJ
{
    public partial class MatrixInOutSelectFrame : UserControl, MatrixInOutSelectFrameView
    {
        #region Properties

        private MatrixChannel _matrixChannelOutput;
        private MatrixChannel _matrixChannelInput;

        /// <summary>
        /// MioFrame 아웃채널정보 
        /// </summary>
        public MatrixChannel MatrixChannelOutput
        {
            get { return _matrixChannelOutput; }
            set
            {
                if(_matrixChannelOutput != value)
                {
                    if (_matrixChannelOutput != null)
                    {
                        _matrixChannelOutput.MatrixChannelValueChanged -= _matrixChannelInput_MatrixChannelValueChanged;
                    }
                    _matrixChannelOutput = value;
                    _matrixChannelOutput.MatrixChannelValueChanged += _matrixChannelInput_MatrixChannelValueChanged;
                    UpdateChannelText(); 

                    if (_matrixChannelInput.Port > 0)
                    {
                        RouteNoChange?.Invoke(_matrixChannelInput, _matrixChannelOutput);
                    }
                }
            }
        }

        /// <summary>
        /// MioFrame 인풋채널 정보 
        /// </summary>
        public MatrixChannel MatrixChannelInput
        {
            get { return _matrixChannelInput; }
            set
            {                
                if (_matrixChannelInput != value)
                {
                    if (_matrixChannelInput != null)
                    {
                        _matrixChannelInput.MatrixChannelValueChanged -= _matrixChannelInput_MatrixChannelValueChanged;
                    }                    
                    _matrixChannelInput = value;
                    RouteNoChange?.Invoke(_matrixChannelInput, _matrixChannelOutput);
                    _matrixChannelInput.MatrixChannelValueChanged += _matrixChannelInput_MatrixChannelValueChanged;
                    UpdateChannelText();
                }
            }
        }

        public Form GetMainForm()
        {
            return this.FindForm();
        }

        public Point GetPositionInForm()
        {
            Form parentForm = this.FindForm();
            Point Position = this.PointToScreen(Point.Empty);
            return parentForm.PointToClient(Position);
        }

        #endregion

        #region 초기화 Methods
        public MatrixInOutSelectFrame()
        {
            InitializeComponent();
            _matrixChannelInput = new MatrixChannel
            {
                Port = 0,
                ChannelName = "-",
                ChannelType = "INPUT",
                RouteNo = 0
            };

            _matrixChannelOutput = new MatrixChannel
            {
                Port = 0,
                ChannelName = "-",
                ChannelType = "OUTPUT",
                RouteNo = 0
            };
            InitiallzeEvent();
        }

        private void InitiallzeEvent()
        {
            lblOutput.MouseDown += MioFrame_MouseDown;
            lblOutput.MouseMove += MioFrame_MouseMove;
            lblOutput.MouseUp += MioFrame_MouseUp;

            lblInput.MouseDown += MioFrame_MouseDown;
            lblInput.MouseMove += MioFrame_MouseMove;
            lblInput.MouseUp += MioFrame_MouseUp;
        }

        private void _matrixChannelInput_MatrixChannelValueChanged(object sender)
        {
            UpdateChannelText();
        }

        #endregion

        #region Private Methods
        public void UpdateChannelText()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateChannelText()));
            }

            lblOutput.Text = _matrixChannelOutput.ChannelName;
            lblInput.Text = _matrixChannelInput.ChannelName;
        }        

        #endregion

        #region Event Handles

        //MioFrame 크기조절 관련 전역변수
        private int GRIPSIZE = 10;                // 간격(마우스포인트 - 라벨 테두리 범위)
        private bool _isGrip = false;                   // 크기조절 시작해도 되는지 확인
        private bool _isClick = false;                  // 크기조절 시작확인
        private string _mioGripPosition = string.Empty; // 크기조절시 상하좌우 등 위치확인

        /// <summary>
        /// MioFrame 크기조절 시작(MioResizeStarted) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MioFrame_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_isGrip)
                {
                    MatrixChannel mc = sender as MatrixChannel;
                    if (mc == _matrixChannelInput)
                    {
                        MioResizeStarted?.Invoke(this, new MioFrameResizeEventClass(new Point(e.Location.X, e.Location.Y + lblOutput.Height), _mioGripPosition));
                    }
                    else
                    {
                        MioResizeStarted?.Invoke(this, new MioFrameResizeEventClass(e.Location, _mioGripPosition));
                    }

                    _isClick = true;
                }
            }
        }

        private void MioFrame_MouseMove(object? sender, MouseEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;

            var mousePosition = this.PointToClient(Cursor.Position);
            var controlBounds = this.ClientRectangle;

            if (!_isClick)
            {
                if (mousePosition.X < GRIPSIZE && mousePosition.Y < GRIPSIZE)
                {
                    // 좌측 상단 모서리
                    control.Cursor = Cursors.SizeNWSE;
                    _mioGripPosition = "좌상";
                    _isGrip = true;
                }
                else if (mousePosition.X > controlBounds.Width - GRIPSIZE && mousePosition.Y > controlBounds.Height - GRIPSIZE)
                {
                    // 우측 하단 모서리
                    control.Cursor = Cursors.SizeNWSE;
                    _mioGripPosition = "우하";
                    _isGrip = true;
                }
                else if (mousePosition.X < GRIPSIZE && mousePosition.Y > controlBounds.Height - GRIPSIZE)
                {
                    // 좌측 하단 모서리
                    control.Cursor = Cursors.SizeNESW;
                    _mioGripPosition = "좌하";
                    _isGrip = true;
                }
                else if (mousePosition.X > controlBounds.Width - GRIPSIZE && mousePosition.Y < GRIPSIZE)
                {
                    // 우측 상단 모서리
                    control.Cursor = Cursors.SizeNESW;
                    _mioGripPosition = "우상";
                    _isGrip = true;
                }
                else if (mousePosition.X < GRIPSIZE || mousePosition.X > controlBounds.Width - GRIPSIZE)
                {
                    // 좌우측 모서리
                    control.Cursor = Cursors.SizeWE;
                    _mioGripPosition = "좌우";
                    _isGrip = true;
                }
                else if (mousePosition.Y < GRIPSIZE || mousePosition.Y > controlBounds.Height - GRIPSIZE)
                {
                    // 상하측 모서리
                    control.Cursor = Cursors.SizeNS;
                    _mioGripPosition = "상하";
                    _isGrip = true;
                }
                else
                {
                    // 커서가 모서리에서 멀어지면 기본 커서로 복구
                    control.Cursor = Cursors.Default;
                    _mioGripPosition = string.Empty;
                    _isGrip = false;
                }
            }
            else if(_isGrip && _isClick)
            {
                MioResizeMove?.Invoke(this, new MioFrameResizeEventClass(new Point(e.Location.X, e.Location.Y + lblOutput.Height), _mioGripPosition));
            }
        }

        private void MioFrame_MouseUp(object? sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            if(e.Button == MouseButtons.Right)
            {
                cms.Show(MousePosition);
            }

            if (_isGrip)
            {
                MioResizeFinished?.Invoke(this, new MioFrameResizeEventClass(new Point(e.Location.X, e.Location.Y + lblOutput.Height), _mioGripPosition));
                _isGrip = false;
                _isClick = false;
                _mioGripPosition = string.Empty;
                //control.Cursor = Cursors.Default;
            }
            else
            {
                if (lbl.Name == "lblInput")
                {
                    InputClick?.Invoke(this, e);
                }
                else if (lbl.Name == "lblOutput")
                {
                    OutputClick?.Invoke(this, e);
                }
            }
        }

        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MioFrameDelete?.Invoke(this, e);
        }

        #endregion

        public event EventHandler InputClick;
        public event EventHandler OutputClick;

        public event EventHandler<MioFrameResizeEventClass> MioResizeStarted;
        public event EventHandler<MioFrameResizeEventClass> MioResizeMove;
        public event EventHandler<MioFrameResizeEventClass> MioResizeFinished;

        public event EventHandler MioFrameDelete;

        public event MatrixInOutSelectFrameView.delRouteNoChange RouteNoChange;
    }
}
