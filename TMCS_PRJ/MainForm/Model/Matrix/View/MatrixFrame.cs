using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace LshMatrix
{

    public partial class MatrixFrame : UserControl, IMFrame
    {
        Panel pnMainScreen;

        public MatrixFrame()
        {
            InitializeComponent();
            pnMainScreen = new Panel();
            InitializeMainScreen();
        }


        private void InitializeMainScreen()
        {
            this.Controls.Add(pnMainScreen);
            pnMainScreen.Paint += PnMainScreen_Paint;
            pnMainScreen.Dock = DockStyle.Fill;
            pnMainScreen.Margin = new Padding(0, 0, 0, 0);
            pnMainScreen.MouseWheel += PnMainScreen_MouseWheel;
        }
        private void PnMainScreen_Paint(object? sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            Color borderColor = Color.FromArgb(80, 80, 80); // RGB(80, 80, 80) 색상
            panel.BackColor = Color.FromArgb(50, 50, 50);
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                                    borderColor,   // 테두리 색상
                                    ButtonBorderStyle.Solid); // 테두리 스타일
        }

        public void SetMatrixFrameChannelList(DataTable dataTable)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate ()
                {
                    SetMatrixFrameChannelList(dataTable);
                }));
            }
            else
            {
                UpdateMainScreen(dataTable);
                CheckCreateScrollBar();
                ClearClickedChannel();
            }            
        }

        readonly int CHANNEL_HEIGHT = 30;

        private void UpdateMainScreen(DataTable dataTable)
        {
            pnMainScreen.Controls.Clear();
            Debug.WriteLine(dataTable.Rows.Count);

            int channelTypeWidth = (pnMainScreen.Width / 3) * 2;

            int rowsCount = dataTable.Rows.Count;

            Label headerlbl1 = new Label
            {
                Text = "신 호",
                Font = new Font("맑은 고딕", 10, FontStyle.Regular),
                Tag = -1,
                Location = new Point(0, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(channelTypeWidth / 2, CHANNEL_HEIGHT)
            };
            pnMainScreen.Controls.Add(headerlbl1);

            Label headerlbl2 = new Label
            {
                Text = "소스명",
                Font = new Font("맑은 고딕", 10, FontStyle.Regular),
                Tag = -1,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(headerlbl1.Width, 0),
                Size = new Size(channelTypeWidth + 1, CHANNEL_HEIGHT)
            };
            pnMainScreen.Controls.Add(headerlbl2);


            for (int i = 0; i < rowsCount; i++)
            {
                Label lbl1 = new Label
                {
                    Text = dataTable.Rows[i][0].ToString(),
                    Tag = i,
                    Font = new Font("맑은 고딕", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(0, CHANNEL_HEIGHT * (i + 1)),
                    Size = new Size(channelTypeWidth / 2, CHANNEL_HEIGHT)
                };
                pnMainScreen.Controls.Add(lbl1);

                Label lbl2 = new Label
                {
                    Text = dataTable.Rows[i][1].ToString(),
                    Tag = i,
                    Font = new Font("맑은 고딕", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(lbl1.Width, CHANNEL_HEIGHT * (i + 1)),
                    Size = new Size(channelTypeWidth + 1, CHANNEL_HEIGHT)
                };
                lbl2.MouseDown += MFrameChannel_MouseDown;
                lbl2.MouseMove += MFrameChannel_MouseMove;
                lbl2.MouseUp += MFrameChannel_MouseUp;
                lbl2.MouseEnter += MFrameChannel_MouseEnter;
                lbl2.MouseLeave += MFrameChannel_MouseLeave;

                pnMainScreen.Controls.Add(lbl2);
            }
        }

        // ------------------------------------------------------------------------------휠 / 스크롤바 이벤트 ---------------------------------------------------------------------------------------

        #region 휠 / 스크롤바 이벤트

        private int scrollPosition = 10;
        private int wheelCount = 0;
        private int maxCount = 0;
        private int maxCountPer = 0;

        private bool isDragging = false;
        private int initialY;

        private void CheckCreateScrollBar()
        {
            wheelCount = 0;
            maxCount = 0;
            maxCountPer = 0;
            Control con = new Control();
            foreach (Control control in pnMainScreen.Controls)
            {
                if ((int)control.Tag > maxCount)
                {
                    con = control;
                }
            }
            if (con == null)
            {
                return;
            }
            maxCount = ((con.Top + con.Height) - pnMainScreen.Height) / scrollPosition;
            maxCountPer = ((con.Top + con.Height) - pnMainScreen.Height) % scrollPosition;

            if (maxCount > 0)
            {
                Panel pn = new Panel
                {
                    Name = "test",
                    Location = new Point(pnMainScreen.Width - 18, 0),
                    Size = new Size(18, pnMainScreen.Height),
                    BackColor = Color.FromArgb(100, 100, 100),
                };
                pnMainScreen.Controls.Add(pn);

                Panel pn2 = new Panel
                {
                    Location = new Point(3, 3),
                    Size = new Size(pn.Width - 6, pn.Height / maxCount),
                    BackColor = Color.FromArgb(140, 140, 140),
                };
                pn.Controls.Add(pn2);
                pn2.MouseDown += ScrollBar_MouseDown;
                pn2.MouseMove += ScrollBar_MouseMove;
                pn2.MouseUp += ScrollBar_MouseUp;
                pn2.MouseEnter += ScrollBar_MouseEnter;
                pn2.MouseLeave += ScrollBar_MouseLeave;

                pn.BringToFront();
            }
        }

        private void PnMainScreen_MouseWheel(object? sender, MouseEventArgs e)
        {
            int tmpScrollValue = 0;

            if (e.Delta > 0)
            {
                if (wheelCount <= 0)
                {
                    return;
                }
                if (wheelCount == maxCount)
                {
                    wheelCount -= 1;
                    tmpScrollValue = -(maxCountPer + scrollPosition);
                }
                else
                {
                    wheelCount -= 1;
                    tmpScrollValue = -scrollPosition;
                }
            }
            else
            {
                if (wheelCount >= maxCount)
                {
                    return;
                }

                if (wheelCount == maxCount - 1)
                {
                    wheelCount += 1;
                    tmpScrollValue = maxCountPer + scrollPosition;
                }
                else
                {
                    wheelCount += 1;
                    tmpScrollValue = scrollPosition;
                }
            }
            UpdateMainScreenWheelMove(tmpScrollValue);
        }

        private void ScrollBar_MouseEnter(object? sender, EventArgs e)
        {
            Panel scrollbar = sender as Panel;
            scrollbar.BackColor = Color.FromArgb(200, 200, 200);
        }

        private void ScrollBar_MouseLeave(object? sender, EventArgs e)
        {
            if (isDragging)
            {
                return;
            }
            Panel scrollbar = sender as Panel;
            scrollbar.BackColor = Color.FromArgb(140, 140, 140);
        }

        private void ScrollBar_MouseDown(object? sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Panel scrollbar = sender as Panel;
                isDragging = true;
                initialY = e.Y;
            }
        }

        private void ScrollBar_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                initialY = 0;
            }
        }        

        private void ScrollBar_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int value = 0;
                Panel scrollbar = sender as Panel;
                Panel scrollbarParent = scrollbar.Parent as Panel;
                int scrollbarHeight = scrollbar.Height;
                int maxScroll = scrollbarParent.Height - scrollbarHeight - 6;
                int deltaY = (e.Y - initialY);
                int scrollStep = maxScroll / maxCount;

                Debug.WriteLine("테스트   : " + e.Y);
                Debug.WriteLine("테스트2  : " + initialY);
                if (e.Y - initialY >= scrollbarHeight / maxCount)
                {
                    if (wheelCount >= maxCount)
                    {
                        return;
                    }

                    if (wheelCount == maxCount - 1)
                    {
                        wheelCount += 1;
                        value = maxCountPer + scrollPosition;
                    }
                    else
                    {
                        wheelCount += 1;
                        value = scrollPosition;
                    }
                    UpdateMainScreenWheelMove(value);
                }
                else if (initialY - e.Y >= scrollbarHeight / maxCount)
                {
                    if (wheelCount <= 0)
                    {
                        return;
                    }
                    if (wheelCount == maxCount)
                    {
                        wheelCount -= 1;
                        value = -(maxCountPer + scrollPosition);
                    }
                    else
                    {
                        wheelCount -= 1;
                        value = -scrollPosition;
                    }
                    UpdateMainScreenWheelMove(value);
                }
            }
        }

        private void UpdateMainScreenWheelMove(int scrollValue)
        {
            foreach (Control control in pnMainScreen.Controls)
            {
                if (control is Label && (int)control.Tag != -1)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y - (scrollValue));
                }

                if (control is Panel)
                {
                    foreach (Control panCon in control.Controls)
                    {
                        int scrollbarHeight = panCon.Height;
                        int maxScroll = control.Height - scrollbarHeight - 6;
                        int scrollbarY = 3 + (int)((double)wheelCount / maxCount * maxScroll);
                        panCon.Location = new Point(panCon.Location.X, scrollbarY);
                    }
                }
            }
        }

        #endregion

        // ------------------------------------------------------------------------------드래그, 클릭, 우클릭 이벤트---------------------------------------------------------------------------------

        #region 드래그, 클릭, 우클릭 이벤트

        private void MFrameChannel_MouseEnter(object? sender, EventArgs e)
        {
            if (_isDragMouseMove )
            {
                return;
            }
            Label lbl = sender as Label;

            if (lbl != _clickedChannel)
            {
                lbl.BackColor = Color.FromArgb(120, 120, 120);
            }
        }

        private void MFrameChannel_MouseLeave(object? sender, EventArgs e)
        {
            if (_isDragMouseMove)
            {
                return;
            }

            Label lbl = sender as Label;

            if(lbl != _clickedChannel)
            {
                lbl.BackColor = Color.Transparent;
            }
            
        }

        private void ChangeClickedChannel()
        {            
            _clickedChannel.BackColor = Color.FromArgb(100,120,180);
            _clickedChannel.Font = new Font("맑은 고딕", 11, FontStyle.Bold);
            ClickedChannelChanged?.Invoke(_clickedChannel, EventArgs.Empty);
        }

        public void ClearClickedChannel()
        {
            if (_clickedChannel != null)
            {
                _clickedChannel.BackColor = Color.Transparent;
                _clickedChannel.Font = new Font("맑은 고딕", 10, FontStyle.Regular);
                _clickedChannel = null;
                ClickedChannelChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        private bool _isDragMouseMove = false;      //드래그관련 마우스 무브
        private Label? _clickedChannel = null;
        private Label? _draglbl = null;
        private Point? _clickedPoint = null; //드래그관련 시작 마우스 위치

        private void MFrameChannel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (_isDragMouseMove && e.Button == MouseButtons.Right)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                Label lbl = sender as Label;
                if (_clickedChannel != null)
                {
                    ClearClickedChannel();
                }
                _clickedPoint = MousePosition;
                _clickedChannel = lbl;
                ChangeClickedChannel();
            }
        }


        private void MFrameChannel_MouseMove(object? sender, MouseEventArgs e)
        {
            Point point;
           
            if (_isDragMouseMove)
            {
                point = new Point(MousePosition.X - (_draglbl.Width / 2), MousePosition.Y - (_draglbl.Height / 2));
                _draglbl.Location = FindForm().PointToClient(point);
                return;
            }
            if (_clickedPoint.HasValue)
            {
                Point startPoint = _clickedPoint.Value;
                Point nowPoint = MousePosition;

                if (GetDistance(startPoint, nowPoint) >= 10)
                {                   
                    ChangeClickedChannel();
                    _isDragMouseMove = true;
                    _draglbl = new Label();
                    _draglbl.Text = _clickedChannel.Text;
                    point = new Point(MousePosition.X - (_draglbl.Width / 2), MousePosition.Y - (_draglbl.Height / 2));
                    _draglbl.Location = FindForm().PointToClient(point);
                    _draglbl.TextAlign = ContentAlignment.MiddleCenter;
                    _draglbl.BackColor = Color.FromArgb(140, 140, 140);
                    _draglbl.BorderStyle = BorderStyle.FixedSingle;
                    _draglbl.ForeColor = Color.White;
                    _draglbl.Size = new Size(50, 50);                    
                    FindForm().Controls.Add(_draglbl);
                    _draglbl.BringToFront();
                    _draglbl.MouseMove += _draglbl_MouseMove;
                    _draglbl.MouseUp += _draglbl_MouseUp;

                }
            }
        }

        private void _draglbl_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!_isDragMouseMove)
                {
                    //ChangeClickedChannel();
                }
                else
                {
                    _draglbl.Dispose();
                    _isDragMouseMove = false;
                    MFrameToObjectDragEnded?.Invoke(_clickedChannel, e);
                }
                _clickedPoint = null;
                return;
            }
        }

        private void _draglbl_MouseMove(object? sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            Point point;
            point = new Point(MousePosition.X - (_draglbl.Width / 2), MousePosition.Y - (_draglbl.Height / 2));
            _draglbl.Location = FindForm().PointToClient(point);
            //return;
        }

        private void MFrameChannel_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!_isDragMouseMove)
                {
                    //ChangeClickedChannel();
                }
                else
                {
                    _draglbl.Dispose();
                    _isDragMouseMove = false;
                    MFrameToObjectDragEnded?.Invoke(_clickedChannel, e);                    
                }
                _clickedPoint = null;
                return;
            }
            else if (e.Button == MouseButtons.Right && _isDragMouseMove)
            {
                // 드래그 중 우클릭 이벤트 무시
                return;
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (_isDragMouseMove)
                    {
                        return;
                    }
                    else
                    {
                        ClearClickedChannel();
                        Label lbl = sender as Label;
                        _clickedChannel = lbl;
                        cms.Show(MousePosition);
                    }
                }
            }
        }

        private void 이름바꾸기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_clickedChannel != null)
            {
                RichTextBox tbox = new RichTextBox();
                //TextBox tbox = new TextBox();
                tbox.BackColor = Color.FromArgb(80, 80, 90);
                tbox.BorderStyle = BorderStyle.None;
                tbox.Size = _clickedChannel.Size;
                tbox.Text = _clickedChannel.Text;
                //tbox.back

                tbox.SelectionAlignment = HorizontalAlignment.Center;
                tbox.ForeColor = Color.White;
                tbox.ImeMode = ImeMode.Hangul;
                tbox.KeyDown += Tbox_KeyDown;
                tbox.Font = new Font("맑은 고딕", 11, FontStyle.Bold);
                _clickedChannel.Controls.Add(tbox);

                // 텍스트 선택
                tbox.SelectionStart = 0;
                tbox.SelectionLength = tbox.Text.Length;
                tbox.Focus();                
            }
            //if (sender is ToolStripItem item && item.Owner is ContextMenuStrip contextMenuStrip)
            //{
            //    if (dgvMatrixChannelList.SelectedCells.Count == 1)
            //    {
            //        DataGridViewCell selectedCell = dgvMatrixChannelList.SelectedCells[0];

            //        dgvMatrixChannelList.ReadOnly = false;
            //        selectedCell.ReadOnly = false;

            //        dgvMatrixChannelList.CurrentCell = selectedCell;
            //        dgvMatrixChannelList.BeginEdit(true);
            //    }
            //}
        }

        private void Tbox_KeyDown(object? sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                RichTextBox tbox = sender as RichTextBox;
                _clickedChannel.Text = tbox.Text;
                tbox.Dispose();
                ClickedChannelNameChanged?.Invoke(_clickedChannel, EventArgs.Empty);
                ChangeClickedChannel();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                RichTextBox tbox = sender as RichTextBox;
                tbox?.Dispose();
                ChangeClickedChannel();
            }
        }

        #endregion







        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if ((keyData & Keys.Right) == Keys.Right || (keyData & Keys.Left) == Keys.Left || (keyData & Keys.Up) == Keys.Up || (keyData & Keys.Down) == Keys.Down)
        //    {
        //        return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        public Form GetFindForm()
        {
            return this.FindForm();
        }

        #region Event Handles


        /// <summary>
        /// cms 이름바꾸기 클릭시 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// 우클릭후 이름바꾼후 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvMatrixChannelList_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            //dgvMatrixChannelList.ReadOnly = true;

            //int rowIndex = e.RowIndex;
            //int columnIndex = e.ColumnIndex;

            //this.BeginInvoke(new MethodInvoker(() =>
            //{
            //    dgvMatrixChannelList.Rows[rowIndex].Cells[columnIndex].Selected = true;
            //}));

            //MatrixChannelNameChanged?.Invoke(sender, e);
        }



        /// <summary>
        /// 레이아웃 변경하는 메서드
        /// </summary>
        /// 
        //private void UpdateDgvMatrixChannelListLayOut()
        //{
        //    if (dgvMatrixChannelList.Columns.Count > 0)
        //    {
        //        //dgvMatrixChannelList.ScrollBars = ScrollBars.None;
        //        // 인덱스 컬럼을 비공개
        //        dgvMatrixChannelList.RowHeadersVisible = false;
        //        // 여백없이 화면에 곽차게
        //        dgvMatrixChannelList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //        // 컬럼헤더 중앙정렬
        //        dgvMatrixChannelList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        // row추가 못하게
        //        dgvMatrixChannelList.AllowUserToAddRows = false;
        //        // 행 높이를 수동으로 조절하는 것을 방지
        //        dgvMatrixChannelList.AllowUserToResizeRows = false;
        //        // 열 너비를 수동으로 조절하는 것을 방지
        //        dgvMatrixChannelList.AllowUserToResizeColumns = false;
        //        // 열 헤더 높이를 수동으로 조절하는 것을 방지
        //        dgvMatrixChannelList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        //        // 데이터 수정 못하게
        //        dgvMatrixChannelList.ReadOnly = true;
        //        // 한 번에 하나의 셀만 선택할 수 있도록 설정
        //        dgvMatrixChannelList.SelectionMode = DataGridViewSelectionMode.CellSelect;
        //        // 여러 셀을 동시에 선택하지 못하게 설정
        //        dgvMatrixChannelList.MultiSelect = false;

        //        dgvMatrixChannelList.ColumnHeadersDefaultCellStyle.Font = new Font(dgvMatrixChannelList.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);

        //        // 각 컬럼의 텍스트를 중앙정렬
        //        foreach (DataGridViewColumn column in dgvMatrixChannelList.Columns)
        //        {
        //            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        }

        //        // 어느 컬럼을 선택하던 제일 뒤쪽 컬럼이 선택되게 변경
        //        dgvMatrixChannelList.SelectionChanged += (sender, args) =>
        //        {
        //            if (dgvMatrixChannelList.SelectedCells.Count > 0)
        //            {
        //                int lastColumnIndex = dgvMatrixChannelList.Columns.Count - 1;
        //                dgvMatrixChannelList.CurrentCell = dgvMatrixChannelList.Rows[dgvMatrixChannelList.SelectedCells[0].RowIndex].Cells[lastColumnIndex];
        //            }
        //        };

        //        int availableHeight = dgvMatrixChannelList.Height - dgvMatrixChannelList.ColumnHeadersHeight;

        //        int rowHeight = availableHeight / dgvMatrixChannelList.RowCount;

        //        if (dgvMatrixChannelList.ScrollBars.HasFlag(ScrollBars.Vertical))
        //        {
        //            rowHeight = availableHeight / dgvMatrixChannelList.RowCount; // 다시 계산합니다.
        //        }
        //        foreach (DataGridViewRow row in dgvMatrixChannelList.Rows)
        //        {
        //            row.Height = rowHeight;
        //            row.ReadOnly = true;
        //        }

        //        // 최초 dgv 실행시 선택된 컬럼이 없게 하기
        //        ClearClickedChannel();
        //    }
        //}



        //private void DgvMatrixChannelList_Resize(object? sender, EventArgs e)
        //{
        //    //UpdateDgvMatrixChannelListLayOut();
        //}


        #endregion


        #region Utility Methods
        /// <summary>
        /// 두포인트사이의 거리확인 메서드 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        static double GetDistance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        #endregion


        public event EventHandler ClickedChannelChanged;
        public event EventHandler ClickedChannelNameChanged;
        public event EventHandler MFrameToObjectDragEnded;
    }
}
