namespace LshDlp
{
    internal partial class DlpFrame : UserControl, IDlpFrame
    {
        public event EventHandler DlpClick;

        internal TableLayoutPanel _tlpDlpFrame;
        private DlpStruct _dlpStruct;


        internal DlpFrame(DlpStruct dlpStruct)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            _dlpStruct = dlpStruct;
            Initialize();
        }


        private void Initialize()
        {
            _tlpDlpFrame = new TableLayoutPanel();
            _tlpDlpFrame.Dock = DockStyle.Fill;
            picDlpFrame.Controls.Add(_tlpDlpFrame);
            _tlpDlpFrame.BackColor = Color.Black;
            _tlpDlpFrame.RowCount = _dlpStruct.RowCount;
            _tlpDlpFrame.ColumnCount = _dlpStruct.ColCount;

            for (int i = 0; i < _dlpStruct.RowCount; i++)
            {
                _tlpDlpFrame.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / _tlpDlpFrame.RowCount));
            }

            for (int i = 0; i < _dlpStruct.ColCount; i++)
            {
                _tlpDlpFrame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / _tlpDlpFrame.ColumnCount));
            }

            foreach (Dlp dlp in _dlpStruct.Dlps)
            {
                dlp.Text = dlp.InputChannel.ChannelName;
                dlp.Dock = DockStyle.Fill;
                dlp.Margin = new Padding(0, 0, 0, 0);
                dlp.BorderStyle = BorderStyle.FixedSingle;
                dlp.BackColor = Color.Red;
                dlp.TextAlign = ContentAlignment.MiddleCenter;
                dlp.MouseDown += mouseDown;
                dlp.MouseMove += mouseMove;
                dlp.MouseUp += mouseUp;
                _tlpDlpFrame.Controls.Add(dlp, dlp.Col, dlp.Row);
            }
        }

        void IDlpFrame.SetDlpFrame(string channelType)
        {
            if (channelType == "INPUT")
            {
                foreach (Dlp dlp in _dlpStruct.Dlps)
                {
                    UpdateDlpText(dlp, dlp.InputChannel.ChannelName);
                }
            }
            else if (channelType == "OUTPUT")
            {
                foreach (Dlp dlp in _dlpStruct.Dlps)
                {
                    UpdateDlpText(dlp, "매트릭스 출력포트 : " + dlp.MatrixPort.ToString());
                }
            }
        }

        void IDlpFrame.UpdateDlpTest()
        {
            foreach (Dlp dlp in _dlpStruct.Dlps)
            {
                if (InvokeRequired)
                {
                    dlp.Invoke(new Action(() => dlp.Text = dlp.InputChannel.ChannelName));
                }
                else
                {
                    // 스레드가 안전하므로 UI 업데이트 실행
                    dlp.Text = dlp.InputChannel.ChannelName;
                }
            }
        }

        private void UpdateDlpText(Dlp dlp, string dlpName)
        {
            if (InvokeRequired)
            {
                // 라벨 컨트롤의 생성 스레드에 실행을 위임
                dlp.Invoke(new Action(() => UpdateDlpText(dlp, dlpName)));
            }
            else
            {
                // 스레드가 안전하므로 UI 업데이트 실행
                dlp.Text = dlpName;
            }
        }

        private Control FindParentControl(Control control, Type senderType)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl == null)
                {
                    return null;
                }
                if (childControl.GetType() == senderType)
                {
                    return control;
                }
                else
                {
                    return FindParentControl(childControl, senderType);
                }
            }
            return null;
        }

        private bool _isMore10 = false;
        private Point? _startPoint = null;
        private PictureBox _picDragBox = null;

        private void mouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _startPoint = this.PointToClient(MousePosition);
            }
        }

        private void mouseMove(object? sender, MouseEventArgs e)
        {
            if (_startPoint.HasValue)
            {
                Point startPoint = _startPoint.Value;
                Point nowPoint = this.PointToClient(MousePosition);
                if (!_isMore10)
                {
                    if (GetDistance(startPoint, nowPoint) >= 10)
                    {
                        _isMore10 = true;
                        InitDragPictureBox();
                        InitDragBox();
                        ShowDragPictureBox();
                    }
                }
                else
                {
                    int x = startPoint.X;
                    int y = startPoint.Y;
                    int width = nowPoint.X - x;
                    int height = nowPoint.Y - y;

                    _picDragBox.Bounds = new Rectangle(x, y, width, height);
                }
            }
        }

        private void InitDragPictureBox()
        {
            Bitmap bmp = new Bitmap(picDlpFrame.Width, picDlpFrame.Height);
            picDlpFrame.DrawToBitmap(bmp, new Rectangle(0, 0, picDlpFrame.Width, picDlpFrame.Height));
            picDlpFrame.Image = bmp;
        }

        private void InitDragBox()
        {
            _picDragBox = new PictureBox();
            _picDragBox.Size = new Size(0, 0);
            _picDragBox.BackColor = Color.FromArgb(128, Color.Blue); // 반투명 파란색
            picDlpFrame.Controls.Add(_picDragBox);
        }

        private void ShowDragPictureBox()
        {
            _tlpDlpFrame.Visible = false;
        }

        private void mouseUp(object? sender, MouseEventArgs e)
        {
            if (_startPoint.HasValue)
            {
                _startPoint = null;
                if (_isMore10)
                {
                    _tlpDlpFrame.Visible = true;
                    _picDragBox.Dispose();
                    _isMore10 = false;
                }

                if (e.Button == MouseButtons.Right)
                {
                    cms.Show(MousePosition);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    DlpClick?.Invoke(sender, e);
                }
            }
            else if (!_startPoint.HasValue)
            {
                if (e.Button == MouseButtons.Right)
                {
                    cms.Show(MousePosition);
                }
            }
        }

        /// <summary>
        /// 두 포인트 사이의 거리를 구하는 함수
        /// </summary>
        private static double GetDistance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private void 출력포트확인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Dlp dlp in _dlpStruct.Dlps)
            {
                dlp.BackColor = Color.White;
                dlp.Text = "출력포트 : " + dlp.MatrixPort.ToString();
            }
            Button lbl2 = new Button();
            this.Controls.Add(lbl2);
            lbl2.BringToFront();
            lbl2.BackColor = Color.Black;
            lbl2.AutoSize = false;
            lbl2.ForeColor = Color.White;
            lbl2.Size = new Size(100, 40);
            lbl2.Location = new Point(
                (this.ClientSize.Width - lbl2.Width) / 2,
                (this.ClientSize.Height - lbl2.Height) / 2
            );
            lbl2.Text = "복 귀";

            lbl2.MouseDown += (sender, e) =>
            {
                lbl2.Dispose();
                foreach (Dlp dlp in _dlpStruct.Dlps)
                {
                    if (InvokeRequired)
                    {
                        dlp.Invoke(new Action(() => dlp.Text = dlp.InputChannel.ChannelName));
                        dlp.BackColor = Color.Red;
                    }
                    else
                    {
                        // 스레드가 안전하므로 UI 업데이트 실행
                        dlp.Text = dlp.InputChannel.ChannelName;
                        dlp.BackColor = Color.Red;
                    }
                }
            };
        }


        private void 전체출력포트변경ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
