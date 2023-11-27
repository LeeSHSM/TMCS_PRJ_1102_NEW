using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LshMatrix
{

    public partial class MatrixFrame : UserControl, IMFrame
    {
        public MatrixFrame()
        {
            InitializeComponent();
            InitializeEvent();
            dgvMatrixChannelList.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvMatrixChannelList, true, null);
            dgvMatrixChannelList.Visible = false;
            dgvMatrixChannelList.Dock = DockStyle.None;
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

        private int scrollPosition = 10;
        private int wheelCount = 0;
        private int maxCount = 0;
        private int maxCountPer = 0;
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
                    tmpScrollValue = -(maxCountPer+ scrollPosition);
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
            //}
        }


        private void CheckOverMainScreen()
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

            if(maxCount > 0)
            {
                Panel pn = new Panel
                {
                    Name = "test",
                    Location = new Point(pnMainScreen.Width - 18, 0),
                    Size = new Size(18,pnMainScreen.Height ),
                    BackColor = Color.FromArgb(100,100,100),
                };
                pnMainScreen.Controls.Add(pn);

                Panel pn2 = new Panel
                {
                    Location = new Point(3, 3),
                    Size = new Size (pn.Width - 6, pn.Height / maxCount),
                    BackColor = Color.FromArgb(140,140,140),
                };
                pn.Controls.Add(pn2);
                pn2.MouseDown += Pn2_MouseDown;
                pn2.MouseMove += Pn2_MouseMove;
                pn2.MouseUp += Pn2_MouseUp;
                pn2.MouseEnter += Pn2_MouseEnter;
                pn2.MouseLeave += Pn2_MouseLeave;

                pn.BringToFront();
            }
        }

        private void Pn2_MouseLeave(object? sender, EventArgs e)
        {
            if (isDragging)
            {
                return;
            }
            Panel scrollbar = sender as Panel;
            scrollbar.BackColor = Color.FromArgb(140, 140, 140);
        }

        private void Pn2_MouseEnter(object? sender, EventArgs e)
        {
            Panel scrollbar = sender as Panel;
            scrollbar.BackColor = Color.FromArgb(200,200,200);
        }

        private bool isDragging = false;
        private int initialY;

        private void Pn2_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                initialY = 0;
            }
        }

        private void Pn2_MouseDown(object? sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                Panel scrollbar = sender as Panel;
                isDragging = true;
                initialY = e.Y ;
            }
        }

        private void Pn2_MouseMove(object? sender, MouseEventArgs e)
        {
            //Debug.WriteLine(e.Y);
            if (isDragging)
            {
                int value = 0;
                Panel scrollbar = sender as Panel;
                Panel scrollbarParent = scrollbar.Parent as Panel;
                int scrollbarHeight = scrollbar.Height;
                int maxScroll = scrollbarParent.Height - scrollbarHeight - 6;
                int deltaY = (e.Y - initialY);
                int scrollStep = maxScroll / maxCount;

                //scrollbar.Location = new Point(scrollbar.Location.X, e.Y - scrollbar.Height );

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

        int height = 30;

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
                Location = new Point(1, 1),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(channelTypeWidth / 2, height)
            };
            pnMainScreen.Controls.Add(headerlbl1);

            Label headerlbl2 = new Label
            {
                Text = "소스명",
                Font = new Font("맑은 고딕", 10, FontStyle.Regular),
                Tag = -1,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(headerlbl1.Width + 1, 1),
                Size = new Size(channelTypeWidth, height)
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
                    Location = new Point(1, 1 + (height * (i + 1))),
                    Size = new Size(channelTypeWidth / 2, height)
                };
                pnMainScreen.Controls.Add(lbl1);

                Label lbl2 = new Label
                {
                    Text = dataTable.Rows[i][1].ToString(),
                    Tag = i,
                    Font = new Font("맑은 고딕", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(lbl1.Width + 1, 1 + (height * (i + 1))),
                    Size = new Size(channelTypeWidth, height)
                };
                pnMainScreen.Controls.Add(lbl2);
            }
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

        Panel pnMainScreen;

        private void InitializeEvent()
        {
            dgvMatrixChannelList.SelectionChanged += DgvMatrixChannelList_SelectionChanged;
            dgvMatrixChannelList.MouseDown += DgvMatrixChannelList_MouseDown;
            dgvMatrixChannelList.MouseMove += DgvMatrixChannelList_MouseMove;
            dgvMatrixChannelList.MouseUp += DgvMatrixChannelList_MouseUp;
            dgvMatrixChannelList.CellMouseUp += DgvMatrixChannelList_CellMouseUp;
            dgvMatrixChannelList.Resize += DgvMatrixChannelList_Resize;
            dgvMatrixChannelList.CellEndEdit += DgvMatrixChannelList_CellEndEdit;


            dgvMatrixChannelList.TabStop = false;
        }

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

        /// <summary>
        /// dgv 내용물 채우는 메서드 
        /// </summary>
        /// <param name="dataTable"></param>
        public void SetMatrixFrameChannelList(DataTable dataTable)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate ()
                {
                    dgvMatrixChannelList.DataSource = dataTable;
                    UpdateMainScreen(dataTable);
                    UpdateDgvMatrixChannelListLayOut();
                }));
            }
            else
            {
                dgvMatrixChannelList.DataSource = dataTable;
                UpdateMainScreen(dataTable);
                UpdateDgvMatrixChannelListLayOut();
            }
            CheckOverMainScreen();
        }



        /// <summary>
        /// dgv 선택한거 없애주는 메서드 
        /// </summary>
        public void ClearClickedCell()
        {
            dgvMatrixChannelList.ClearSelection();
        }

        /// <summary>
        /// 레이아웃 변경하는 메서드
        /// </summary>
        /// 
        private void UpdateDgvMatrixChannelListLayOut()
        {
            if (dgvMatrixChannelList.Columns.Count > 0)
            {
                //dgvMatrixChannelList.ScrollBars = ScrollBars.None;
                // 인덱스 컬럼을 비공개
                dgvMatrixChannelList.RowHeadersVisible = false;
                // 여백없이 화면에 곽차게
                dgvMatrixChannelList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // 컬럼헤더 중앙정렬
                dgvMatrixChannelList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // row추가 못하게
                dgvMatrixChannelList.AllowUserToAddRows = false;
                // 행 높이를 수동으로 조절하는 것을 방지
                dgvMatrixChannelList.AllowUserToResizeRows = false;
                // 열 너비를 수동으로 조절하는 것을 방지
                dgvMatrixChannelList.AllowUserToResizeColumns = false;
                // 열 헤더 높이를 수동으로 조절하는 것을 방지
                dgvMatrixChannelList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                // 데이터 수정 못하게
                dgvMatrixChannelList.ReadOnly = true;
                // 한 번에 하나의 셀만 선택할 수 있도록 설정
                dgvMatrixChannelList.SelectionMode = DataGridViewSelectionMode.CellSelect;
                // 여러 셀을 동시에 선택하지 못하게 설정
                dgvMatrixChannelList.MultiSelect = false;

                dgvMatrixChannelList.ColumnHeadersDefaultCellStyle.Font = new Font(dgvMatrixChannelList.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);

                // 각 컬럼의 텍스트를 중앙정렬
                foreach (DataGridViewColumn column in dgvMatrixChannelList.Columns)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // 어느 컬럼을 선택하던 제일 뒤쪽 컬럼이 선택되게 변경
                dgvMatrixChannelList.SelectionChanged += (sender, args) =>
                {
                    if (dgvMatrixChannelList.SelectedCells.Count > 0)
                    {
                        int lastColumnIndex = dgvMatrixChannelList.Columns.Count - 1;
                        dgvMatrixChannelList.CurrentCell = dgvMatrixChannelList.Rows[dgvMatrixChannelList.SelectedCells[0].RowIndex].Cells[lastColumnIndex];
                    }
                };

                int availableHeight = dgvMatrixChannelList.Height - dgvMatrixChannelList.ColumnHeadersHeight;

                int rowHeight = availableHeight / dgvMatrixChannelList.RowCount;

                if (dgvMatrixChannelList.ScrollBars.HasFlag(ScrollBars.Vertical))
                {
                    rowHeight = availableHeight / dgvMatrixChannelList.RowCount; // 다시 계산합니다.
                }
                foreach (DataGridViewRow row in dgvMatrixChannelList.Rows)
                {
                    row.Height = rowHeight;
                    row.ReadOnly = true;
                }

                // 최초 dgv 실행시 선택된 컬럼이 없게 하기
                ClearClickedCell();
            }
        }



        #region Event Handles

        /// <summary>
        /// dgv 셀렉한거 바뀔때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvMatrixChannelList_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvMatrixChannelList.SelectedCells.Count > 0 && dgvMatrixChannelList.SelectedCells[0].ColumnIndex == 1) // 유효한 셀인지 확인
            {
                SelectedCellChanged?.Invoke(sender, e);
            }
            else if (dgvMatrixChannelList.SelectedCells.Count == 0)
            {
                SelectedCellChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 셀 우클릭 했을때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvMatrixChannelList_CellMouseUp(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                dgvMatrixChannelList.CurrentCell = dgvMatrixChannelList.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // DataGridView의 현재 좌표를 스크린 좌표로 변환합니다.
                Point currentCellLocation = dgvMatrixChannelList.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;

                currentCellLocation.Offset(e.X, e.Y);

                cms.Show(dgvMatrixChannelList, currentCellLocation);
            }
        }

        /// <summary>
        /// cms 이름바꾸기 클릭시 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 이름바꾸기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item && item.Owner is ContextMenuStrip contextMenuStrip)
            {
                if (dgvMatrixChannelList.SelectedCells.Count == 1)
                {
                    DataGridViewCell selectedCell = dgvMatrixChannelList.SelectedCells[0];

                    dgvMatrixChannelList.ReadOnly = false;
                    selectedCell.ReadOnly = false;

                    dgvMatrixChannelList.CurrentCell = selectedCell;
                    dgvMatrixChannelList.BeginEdit(true);
                }
            }
        }

        /// <summary>
        /// 우클릭후 이름바꾼후 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvMatrixChannelList_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            dgvMatrixChannelList.ReadOnly = true;

            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            this.BeginInvoke(new MethodInvoker(() =>
            {
                dgvMatrixChannelList.Rows[rowIndex].Cells[columnIndex].Selected = true;
            }));

            MatrixChannelNameChanged?.Invoke(sender, e);
        }



        //드래그 관련 전역변수
        private bool _isDragMouseMove = false;      //드래그관련 마우스 무브
        private Point? _pDragStartedPostion = null; //드래그관련 시작 마우스 위치
        private Label _dragLbl;                     //드래그 라벨
        private Form mainForm;                              //부모폼 확인

        /// <summary>
        /// DragStarted 이벤트... 드래그 시작할때? 이건 마우스 좌클릭할때 무조건 동작 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvMatrixChannelList_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _pDragStartedPostion = MousePosition;
            }
        }

        /// <summary>
        /// DragMove 이벤트... 드래그 시작후 마우스 움직일때만 동작 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvMatrixChannelList_MouseMove(object? sender, MouseEventArgs e)
        {
            Point point;
            if (_isDragMouseMove)
            {
                point = this.PointToScreen(new Point(e.X - (_dragLbl.Width / 2), e.Y - (_dragLbl.Height / 2)));
                _dragLbl.Location = mainForm.PointToClient(point);
                return;
            }
            if (_pDragStartedPostion.HasValue)
            {
                Point startPoint = _pDragStartedPostion.Value;
                Point nowPoint = MousePosition;

                if (GetDistance(startPoint, nowPoint) >= 10)
                {
                    _isDragMouseMove = true;

                    mainForm = this.FindForm();
                    _dragLbl = new Label();
                    _dragLbl.Text = dgvMatrixChannelList.SelectedCells[0].Value.ToString();
                    _dragLbl.TextAlign = ContentAlignment.MiddleCenter;
                    point = this.PointToScreen(new Point(e.X - (_dragLbl.Width / 2), e.Y - (_dragLbl.Height / 2)));
                    _dragLbl.Location = mainForm.PointToClient(point); // 위치 변환
                    _dragLbl.BackColor = Color.Transparent;
                    _dragLbl.BorderStyle = BorderStyle.FixedSingle;
                    _dragLbl.Size = new Size(50, 50);
                    mainForm.Controls.Add(_dragLbl);
                    _dragLbl.BringToFront();
                }
            }
        }

        /// <summary>
        /// DrageEnded 이벤트... 마우스 업 했을때.. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvMatrixChannelList_MouseUp(object? sender, MouseEventArgs e)
        {
            if (_isDragMouseMove)
            {
                _dragLbl.Dispose();
                MFrameToObjectDragEnded?.Invoke(dgvMatrixChannelList, e);
            }

            _isDragMouseMove = false;
            _pDragStartedPostion = null;
            mainForm = null;
        }

        private void DgvMatrixChannelList_Resize(object? sender, EventArgs e)
        {
            UpdateDgvMatrixChannelListLayOut();
        }


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


        public event EventHandler SelectedCellChanged;
        public event EventHandler MatrixChannelNameChanged;
        public event EventHandler MFrameToObjectDragEnded;
    }
}
