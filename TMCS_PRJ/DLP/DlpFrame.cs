

namespace LshDlp
{
    internal partial class DlpFrame : UserControl, DlpFrameView
    {
        internal event EventHandler DlpClick;
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
                dlp.MatrixPort = 0;
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

        void DlpFrameView.SetDlpFrame(string channelType)
        {
            if(channelType == "INPUT")
            {
                foreach (Dlp dlp in _dlpStruct.Dlps)
                {
                    UpdateDlpText(dlp, dlp.InputChannel.ChannelName);
                }
            }
            else if(channelType == "OUTPUT")
            {
                foreach(Dlp dlp in _dlpStruct.Dlps)
                {
                    UpdateDlpText(dlp, "매트릭스 출력포트 : "+dlp.MatrixPort.ToString());
                }
            }
        }


        private void UpdateDlpText(Dlp dlp, string dlpName)
        {
            if (InvokeRequired)
            {
                // 라벨 컨트롤의 생성 스레드에 실행을 위임
                dlp.Invoke(new Action(() => UpdateDlpText(dlp,dlpName)));
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
            _startPoint = null;
            if (_isMore10)
            {
                _tlpDlpFrame.Visible = true;
                _picDragBox.Dispose();                
                _isMore10 = false;
            }
        }

        //private void mouseUp(object sender, EventArgs e)
        //{
        //    if (_dragPanel != null)
        //    {
        //        // 비교할 판넬 설정
        //        Rectangle panelBounds = _dragPanel.Bounds;

        //        // 현재 패널 안에 있는 모든 라벨을 가져옵니다.
        //        List<DLP> dLPs = DLPCover.Controls.OfType<DLP>().ToList();
        //        List<DLP> overlappingDlps = new List<DLP>();

        //        // 각 라벨에 대해 패널과 겹치는지 확인합니다.
        //        foreach (DLP dLP in dLPs)
        //        {
        //            // 라벨의 Bounds를 가져옵니다.
        //            Rectangle labelBounds = dLP.Bounds;

        //            // 라벨이 패널과 겹치는지 확인
        //            if (panelBounds.IntersectsWith(labelBounds))
        //            {
        //                overlappingDlps.Add(dLP);
        //            }
        //        }

        //        if (overlappingDlps.Count > 1 && !overlappingDlps[0].DragChecked)
        //        {
        //            //클릭시점을 기준으로 베이스 될 dlp선택
        //            var baseDlp = overlappingDlps[0];
        //            baseDlp.DragChecked = true;
        //            baseDlp.BackColor = Color.White;

        //            foreach (DLP dlp in overlappingDlps.Skip(1))
        //            {
        //                // 그룹 내의 첫 번째 컨트롤을 기준으로 합니다.
        //                dlp.Visible = false;
        //                dlp.Dock = DockStyle.None;
        //                if (baseDlp.Row == dlp.Row)
        //                {
        //                    DLPCover.SetColumnSpan(baseDlp, DLPCover.GetColumnSpan(baseDlp) + 1);
        //                }
        //                else if (baseDlp.Col == dlp.Col)
        //                {
        //                    DLPCover.SetRowSpan(baseDlp, DLPCover.GetRowSpan(baseDlp) + 1);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        DlpClick(sender, e);
        //    }

        //    // 드래그박스 관련 초기화
        //    if (_dragPictureBox != null)
        //    {
        //        _dragPictureBox.Dock = DockStyle.None;
        //        _dragPictureBox.Visible = false;
        //        DLPCover.Visible = true;
        //        DLPCover.Dock = DockStyle.Fill;
        //        _dragPictureBox.Image.Dispose();
        //        _dragPictureBox.Dispose();
        //        _dragPictureBox = null;
        //        _dragPanel = null;
        //    }
        //    _startPoint = null;
        //}


        //public void RefreshDlp()
        //{
        //    foreach (DLP dlp in DLPCover.Controls)
        //    {
        //        DLPCover.SetRowSpan(dlp, 1);
        //        DLPCover.SetColumnSpan(dlp, 1);
        //        DLPCover.SetCellPosition(dlp, new TableLayoutPanelCellPosition(dlp.Col, dlp.Row));
        //        dlp.Dock = DockStyle.Fill;
        //        dlp.BackColor = Color.Red;
        //        dlp.Visible = true;
        //        dlp.DragChecked = false;
        //    }
        //}





        /// <summary>
        /// 두 포인트 사이의 거리를 구하는 함수
        /// </summary>
        private static double GetDistance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }


    }
}
