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

        private MatrixChannel _matrixChannelOutput;
        private MatrixChannel _matrixChannelInput;


        private void InitiallzeEvent()
        {
            lblOutput.MouseDown += MioFrame_MouseDown;
            lblOutput.MouseMove += MioFrame_MouseMove;
            lblOutput.MouseUp += MioFrame_MouseUp;

            lblInput.MouseDown += MioFrame_MouseDown;
            lblInput.MouseMove += MioFrame_MouseMove;
            lblInput.MouseUp += MioFrame_MouseUp;

            //tableLayoutPanel1.MouseMove += MioFrame_MouseMove;
        }

        public MatrixChannel MatrixChannelOutput
        {
            get { return _matrixChannelOutput; }
            set
            {
                _matrixChannelOutput = value;
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateMatrixChannel(_matrixChannelOutput)));
                }
                else
                {
                    UpdateMatrixChannel(_matrixChannelOutput);
                }
            }
        }
        public MatrixChannel MatrixChannelInput
        {
            get { return _matrixChannelInput; }
            set
            {
                _matrixChannelInput = value;
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateMatrixChannel(_matrixChannelInput)));
                }
                else
                {
                    UpdateMatrixChannel(_matrixChannelInput);
                }
                RouteNoChange?.Invoke(_matrixChannelInput.Port, _matrixChannelOutput.Port);
                Debug.WriteLine(_matrixChannelInput.Port + " : " + _matrixChannelOutput.Port);
            }
        }

        private void UpdateMatrixChannel(MatrixChannel mc)
        {
            if (mc == _matrixChannelOutput)
            {
                lblOutput.Text = mc.ChannelName;
            }
            else if (mc == _matrixChannelInput)
            {
                lblInput.Text = mc.ChannelName;
            }
        }

        private const int GripSize = 10;
        private bool _isGrip = false;
        private bool _isClick = false;
        private string _mioGripPosition = string.Empty;

        private void MioFrame_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_isGrip)
                {
                    MatrixChannel mc = sender as MatrixChannel;
                    if (mc == _matrixChannelInput)
                    {
                        MioResizeStarted(this, new MioFrameResizeEventClass(new Point(e.Location.X, e.Location.Y + lblOutput.Height), _mioGripPosition));
                    }
                    else
                    {
                        MioResizeStarted(this, new MioFrameResizeEventClass(e.Location, _mioGripPosition));
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
                if (mousePosition.X < GripSize && mousePosition.Y < GripSize)
                {
                    // 좌측 상단 모서리
                    control.Cursor = Cursors.SizeNWSE;
                    _mioGripPosition = "좌상";
                    _isGrip = true;
                }
                else if (mousePosition.X > controlBounds.Width - GripSize && mousePosition.Y > controlBounds.Height - GripSize)
                {
                    // 우측 하단 모서리
                    control.Cursor = Cursors.SizeNWSE;
                    _mioGripPosition = "우하";
                    _isGrip = true;
                }
                else if (mousePosition.X < GripSize && mousePosition.Y > controlBounds.Height - GripSize)
                {
                    // 좌측 하단 모서리
                    control.Cursor = Cursors.SizeNESW;
                    _mioGripPosition = "좌하";
                    _isGrip = true;
                }
                else if (mousePosition.X > controlBounds.Width - GripSize && mousePosition.Y < GripSize)
                {
                    // 우측 상단 모서리
                    control.Cursor = Cursors.SizeNESW;
                    _mioGripPosition = "우상";
                    _isGrip = true;
                }
                else if (mousePosition.X < GripSize || mousePosition.X > controlBounds.Width - GripSize)
                {
                    // 좌우측 모서리
                    control.Cursor = Cursors.SizeWE;
                    _mioGripPosition = "좌우";
                    _isGrip = true;
                }
                else if (mousePosition.Y < GripSize || mousePosition.Y > controlBounds.Height - GripSize)
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
            else
            {
                MioResizeMove(this, new MioFrameResizeEventClass(new Point(e.Location.X, e.Location.Y + lblOutput.Height), _mioGripPosition));
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
                MioResizeFinished(this, new MioFrameResizeEventClass(new Point(e.Location.X, e.Location.Y + lblOutput.Height), _mioGripPosition));
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

        public event EventHandler InputClick;
        public event EventHandler OutputClick;

        public event EventHandler<MioFrameResizeEventClass> MioResizeStarted;
        public event EventHandler<MioFrameResizeEventClass> MioResizeMove;
        public event EventHandler<MioFrameResizeEventClass> MioResizeFinished;

        public event EventHandler MioFrameDelete;

        public event MatrixInOutSelectFrameView.delRouteNoChange RouteNoChange;
    }
}
