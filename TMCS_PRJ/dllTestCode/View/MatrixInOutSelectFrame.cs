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

namespace LshMatrix
{
    public partial class MatrixInOutSelectFrame : UserControl, IMioFrame
    {
        #region Properties

        private MatrixChannel _matrixChannelOutput;
        private MatrixChannel _matrixChannelInput;

        public MatrixInOutSelectFrame MioFrame 
        { 
            get { return this; }
        }

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
                //if (_matrixChannelInput != value)
                //{
                    if (_matrixChannelInput != null)
                    {
                        _matrixChannelInput.MatrixChannelValueChanged -= _matrixChannelInput_MatrixChannelValueChanged;
                    }                    
                    _matrixChannelInput = value;                    
                    _matrixChannelInput.MatrixChannelValueChanged += _matrixChannelInput_MatrixChannelValueChanged;
                    UpdateChannelText();

                    if (_matrixChannelOutput.Port > 0)
                    {
                        RouteNoChange?.Invoke(_matrixChannelInput, _matrixChannelOutput);
                    }                    
                //}
            }
        }

        public string ParentId 
        { 
            get; 
            set;
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
            _matrixChannelOutput = new MatrixChannel
            {
                Port = 0,
                ChannelName = "-",
                ChannelType = "OUTPUT",
                RouteNo = 0
            };

            _matrixChannelInput = new MatrixChannel
            {
                Port = 0,
                ChannelName = "-",
                ChannelType = "INPUT",
                RouteNo = 0
            };
            InitiallzeEvent();
        }

        private void InitiallzeEvent()
        {
            this.ParentChanged += MatrixInOutSelectFrame_ParentChanged;

            lblOutput.MouseDown += MioFrame_MouseDown;
            lblOutput.MouseMove += MioFrame_MouseMove;
            lblOutput.MouseUp += MioFrame_MouseUp;

            lblInput.MouseDown += MioFrame_MouseDown;
            lblInput.MouseMove += MioFrame_MouseMove;
            lblInput.MouseUp += MioFrame_MouseUp;            
        }

        #endregion

        #region Private Methods
        public void UpdateChannelText()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateChannelText()));
                return;
            }

            lblOutput.Text = _matrixChannelOutput.ChannelName;
            lblInput.Text = _matrixChannelInput.ChannelName;
        }

        #endregion

        #region Event Handles

        private void MatrixInOutSelectFrame_ParentChanged(object? sender, EventArgs e)
        {
            var mainForm = this.FindForm();
            if(mainForm != null)
            {
                mainForm.Deactivate -= MainForm_Deactivated;
                mainForm.Deactivate += MainForm_Deactivated;
            }                        
        }

        private void _matrixChannelInput_MatrixChannelValueChanged(object sender, EventArgs e)
        {
            UpdateChannelText();
        }


        //MioFrame 크기조절 관련 전역변수-------------------------------------------------------------------
        private int GRIPSIZE = 8;                // 간격(마우스포인트 - 라벨 테두리 범위)
        private int MAXWIDTH = 150;
        private int MAXHEIGHT = 180;
        private int MINWIDTH = 50;
        private int MINHEIGHT = 50;
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
                    _isClick = true;
                }
            }
        }

        private void MioFrame_MouseMove(object? sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl == null) return;

            if (!_isClick)
            {
                if(lbl.Name == "lblOutput")
                {
                    //if (e.X < GRIPSIZE && e.Y < GRIPSIZE)
                    //{
                    //    // 좌측 상단 모서리
                    //    lbl.Cursor = Cursors.SizeNWSE;
                    //    _mioGripPosition = "좌상";
                    //    _isGrip = true;
                    //}
                    
                    //else if (e.X > controlBounds.Width - GRIPSIZE && e.Y < GRIPSIZE)
                    //{
                    //    // 우측 상단 모서리
                    //    lbl.Cursor = Cursors.SizeNESW;
                    //    _mioGripPosition = "우상";
                    //    _isGrip = true;
                    //}
                    //else if (e.Y < GRIPSIZE)
                    //{
                    //    // 상하측 모서리
                    //    lbl.Cursor = Cursors.SizeNS;
                    //    _mioGripPosition = "상";
                    //    _isGrip = true;
                    //}
                    //else if (e.X < GRIPSIZE || e.X > controlBounds.Width - GRIPSIZE)
                     if (e.X > lbl.Width - GRIPSIZE)
                    {
                        // 좌우측 모서리
                        lbl.Cursor = Cursors.SizeWE;
                        _mioGripPosition = "좌우";
                        _isGrip = true;
                    }
                    else
                    {
                        // 커서가 모서리에서 멀어지면 기본 커서로 복구
                        lbl.Cursor = Cursors.Default;
                        _mioGripPosition = string.Empty;
                        _isGrip = false;
                    }
                }
                else
                {                    
                    if (e.X > lbl.Width - GRIPSIZE && e.Y > lbl.Height - GRIPSIZE)
                    {
                        // 우측 하단 모서리
                        lbl.Cursor = Cursors.SizeNWSE;
                        _mioGripPosition = "우하";
                        _isGrip = true;
                    }
                    //else if (e.X < GRIPSIZE && e.Y > lbl.Height - GRIPSIZE)
                    //{
                    //    // 좌측 하단 모서리
                    //    lbl.Cursor = Cursors.SizeNESW;
                    //    _mioGripPosition = "좌하";
                    //    _isGrip = true;
                    //}
                    else if (e.Y > lbl.Height - GRIPSIZE)
                    {
                        //상하측 모서리
                        lbl.Cursor = Cursors.SizeNS;
                        _mioGripPosition = "하";
                        _isGrip = true;
                    }
                    //else if (e.X < GRIPSIZE || e.X > lbl.Width - GRIPSIZE)
                    else if (e.X > lbl.Width - GRIPSIZE)
                    {
                        // 좌우측 모서리
                        lbl.Cursor = Cursors.SizeWE;
                        _mioGripPosition = "좌우";
                        _isGrip = true;
                    }
                    else
                    {
                        //커서가 모서리에서 멀어지면 기본 커서로 복구
                        lbl.Cursor = Cursors.Default;
                        _mioGripPosition = string.Empty;
                        _isGrip = false;
                    }
                }     
            }
            else if(_isGrip && _isClick)
            {
                DragResized(sender,e);                        
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
                _isGrip = false;
                _isClick = false;
                _mioGripPosition = string.Empty;
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

        private void MainForm_Deactivated(object? sender, EventArgs e)
        {
            if (_isGrip)
            {
                _isGrip = false;
                _isClick = false;
                _mioGripPosition = string.Empty;
            }
        }

        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MioFrameDelete?.Invoke(this, e);
        }

        #endregion

        #region Utility Methods
        private void DragResized(object sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            int x = 0;
            int y = 0;
            if (lbl.Name == "lblOutput")
            {
                switch (_mioGripPosition)
                {
                    case "좌우":
                        x = e.X;
                        y = this.ClientRectangle.Height;
                        break;
                }
            }
            else
            {
                switch (_mioGripPosition)
                {
                    case "좌우":
                        x = e.X;
                        y = this.ClientRectangle.Height;
                        break;
                    case "우하":
                        x = e.X;
                        y = lblOutput.Height + e.Y;
                        break;
                    case "하":
                        x = lbl.Width;
                        y = lblOutput.Height + e.Y;
                        break;
                }
            }
            if (MAXWIDTH < x)
            {
                x = MAXWIDTH;
            }
            if (MAXHEIGHT < y)
            {
                y = MAXHEIGHT;
            }
            if (MINWIDTH > x)
            {
                x = MINWIDTH;
            }
            if (MINHEIGHT > y)
            {
                y = MINHEIGHT;
            }
            this.Size = new Size(x, y);
        }

        #endregion

        public event EventHandler InputClick;
        public event EventHandler OutputClick;
        public event EventHandler MioFrameDelete;
        public event IMioFrame.delRouteNoChange RouteNoChange;
    }
}
