using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMCS_PRJ
{
    public partial class DlpFrame : UserControl,DlpFrameView
    {
        public event EventHandler LayoutRefresh;
        public event EventHandler DlpClick;

        public TableLayoutPanel _tlpDlpFrame;

        public DlpFrame()
        {
            InitializeComponent();
        }

        public void SetDlpFrame(DlpStruct dlpStruct)
        {
            _tlpDlpFrame = new TableLayoutPanel();
            _tlpDlpFrame.Dock = DockStyle.Fill;
            this.Controls.Add(_tlpDlpFrame);
            _tlpDlpFrame.RowCount = dlpStruct.RowCount;
            _tlpDlpFrame.ColumnCount = dlpStruct.ColCount;
            for (int i = 0; i < dlpStruct.RowCount; i++)
            {
                _tlpDlpFrame.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / _tlpDlpFrame.RowCount));
            }

            for (int i = 0; i < dlpStruct.ColCount; i++)
            {
                _tlpDlpFrame.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / _tlpDlpFrame.ColumnCount));
            }

            foreach (Dlp dlp in dlpStruct.Dlps)
            {
                dlp.Text = dlp.Row.ToString() + " : " + dlp.Col.ToString();
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

        private void mouseUp(object? sender, MouseEventArgs e)
        {
           
        }

        private void mouseMove(object? sender, MouseEventArgs e)
        {
            
        }

        private void mouseDown(object? sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// 최초 실행시 화면구성해줄거!!
        /// </summary>
        //public void InitDLPFrame(List<DLP> dlps, int rowCount, int colCount)
        //{
        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        DLPCover.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / DLPCover.RowCount));
        //    }

        //    for (int i = 0; i < colCount; i++)
        //    {
        //        DLPCover.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / DLPCover.ColumnCount));
        //    }
        //    foreach (DLP dlp in dlps)
        //    {
        //        dlp.Text = dlp.Row.ToString() + " : " + dlp.Col.ToString();
        //        dlp.Dock = DockStyle.Fill;
        //        dlp.Margin = new Padding(0, 0, 0, 0);
        //        dlp.BorderStyle = BorderStyle.FixedSingle;
        //        dlp.BackColor = Color.Red;
        //        dlp.TextAlign = ContentAlignment.MiddleCenter;
        //        dlp.MouseDown += mouseDown;
        //        dlp.MouseMove += mouseMove;
        //        dlp.MouseUp += mouseUp;
        //        DLPCover.Controls.Add(dlp, dlp.Col, dlp.Row);
        //    }
        //}

        //private bool _isClick = false;
        //private Point? _startPoint = null;
        //private Panel _dragPanel = null;
        //private PictureBox _dragPictureBox = null;

        //private void mouseDown(object sender, EventArgs e)
        //{
        //    MouseEventArgs mouse = (MouseEventArgs)e;
        //    _isClick = true;

        //    if (mouse.Button == MouseButtons.Left)
        //    {
        //        _startPoint = DLPCover.PointToClient(MousePosition);
        //    }
        //}

        //private void mouseMove(object sender, EventArgs e)
        //{
        //    if (_startPoint.HasValue)
        //    {
        //        Point startPoint = _startPoint.Value;
        //        Point nowPoint = DLPCover.PointToClient(MousePosition);

        //        if (GetDistance(startPoint, nowPoint) >= 10)
        //        {
        //            if (_isClick)
        //            {
        //                _isClick = false;

        //                InitDragPictureBox();

        //                InitDragBox();

        //                ShowDragPictureBox();
        //            }

        //            if (_dragPanel != null)
        //            {
        //                int x = startPoint.X;
        //                int y = startPoint.Y;
        //                int width = nowPoint.X - x;
        //                int height = nowPoint.Y - y;

        //                _dragPanel.Bounds = new Rectangle(x, y, width, height);
        //            }
        //        }
        //    }
        //}

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

        //public void OnLayoutRefresh(object sender, EventArgs e)
        //{
        //    LayoutRefresh(sender, e);
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

        //private void ShowDragPictureBox()
        //{
        //    DLPCover.Visible = false;
        //    DLPCover.Dock = DockStyle.None;
        //    _dragPictureBox.Dock = DockStyle.Fill;
        //    _dragPictureBox.Visible = true;
        //    picDisplayCover.Controls.Add(_dragPictureBox);
        //}
        //private void InitDragPictureBox()
        //{
        //    _dragPictureBox = new PictureBox();

        //    Bitmap bmp = new Bitmap(picDisplayCover.Width, picDisplayCover.Height);
        //    picDisplayCover.DrawToBitmap(bmp, new Rectangle(0, 0, picDisplayCover.Width, picDisplayCover.Height));
        //    _dragPictureBox.Image = bmp;
        //    _dragPictureBox.Size = new Size(picDisplayCover.Width, picDisplayCover.Height);
        //    _dragPictureBox.Location = new Point(0, 0);
        //}
        //private void InitDragBox()
        //{
        //    _dragPanel = new Panel();
        //    _dragPanel.Size = new Size(0, 0);
        //    _dragPanel.BackColor = Color.FromArgb(128, Color.Blue); // 반투명 파란색
        //    _dragPictureBox.Controls.Add(_dragPanel);
        //}

        ///// <summary>
        ///// 두 포인트 사이의 거리를 구하는 함수
        ///// </summary>
        //static double GetDistance(Point p1, Point p2)
        //{
        //    int dx = p2.X - p1.X;
        //    int dy = p2.Y - p1.Y;
        //    return Math.Sqrt(dx * dx + dy * dy);
        //}
    }
}
