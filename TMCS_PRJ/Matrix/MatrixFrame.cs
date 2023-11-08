using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace TMCS_PRJ
{

    public partial class MatrixFrame : UserControl, MatrixFrameView
    {
        #region Properties

        /// <summary>
        /// MFrame 채널타입 변수
        /// </summary>
        private string _nowChannelType;

        /// <summary>
        /// MFrame에서 선택된 채널 변수
        /// </summary>
        private MatrixChannel _selectedChannel;

        /// <summary>
        /// MFrame 채널타입 설정변수
        /// </summary>
        public string NowChannelType
        {
            get { return _nowChannelType; }
            set
            {
                if (value != "INPUT" && value != "OUTPUT")
                { throw new ArgumentException("Channel type must be either 'INPUT' or 'OUTPUT'", nameof(value)); }
                _nowChannelType = value;
            }
        }

        public MatrixChannel SelectedChannel
        {
            get { return _selectedChannel; }
            set { _selectedChannel = value; }
        }

        #endregion

        #region 초기화 Methods 
        public MatrixFrame()
        {
            InitializeComponent();
            InitializeEvent();
            dgvMatrixChannelList.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvMatrixChannelList, true, null);
        }

        private void InitializeEvent()
        {
            dgvMatrixChannelList.MouseDown += DgvMatrixChannelList_MouseDown;
            dgvMatrixChannelList.MouseMove += DgvMatrixChannelList_MouseMove;
            dgvMatrixChannelList.MouseUp += DgvMatrixChannelList_MouseUp;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// dgv 내용물 채우는 메서드 
        /// </summary>
        /// <param name="dataTable"></param>
        public void SetMatrixChannelList(DataTable dataTable)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate ()
                {
                    dgvMatrixChannelList.DataSource = dataTable;
                    UpdateDgvMatrixChannelListLayOut();
                }));
            }
            else
            {
                dgvMatrixChannelList.DataSource = dataTable;
                UpdateDgvMatrixChannelListLayOut();
            }
        }

        /// <summary>
        /// dgv 선택한거 없애주는 메서드 
        /// </summary>
        public void ClearClickedCell()
        {
            dgvMatrixChannelList.ClearSelection();
        }

        #endregion

        #region Private Methods

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
                }

                // 최초 dgv 실행시 선택된 컬럼이 없게 하기
                dgvMatrixChannelList.ClearSelection();
            }
        }
        #endregion


        #region Event Handles

        /// <summary>
        /// dgv 셀렉한거 바뀔때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMatrixChannelList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMatrixChannelList.SelectedCells.Count > 0 && _nowChannelType != null && dgvMatrixChannelList.SelectedCells[0].ColumnIndex == 1) // 유효한 셀인지 확인
            {
                CellClick?.Invoke(sender, e);
            }
            else if (dgvMatrixChannelList.SelectedCells.Count == 0 && _nowChannelType != null)
            {
                CellClick?.Invoke(null, EventArgs.Empty);
            }
        }


        //드래그 관련 전역변수
        private bool _isDragMouseDown = false;      //드래드관련 마우스 다운
        private bool _isDragMouseMove = false;      //드래그관련 마우스 무브
        private Point? _pDragStartedPostion = null; //드래그관련 시작 마우스 위치

        /// <summary>
        /// DragStarted 이벤트... 드래그 시작할때? 이건 마우스 좌클릭할때 무조건 동작 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvMatrixChannelList_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragMouseDown = true;
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
            //dgvMatrixChannelList.Update();
            if (_pDragStartedPostion.HasValue)
            {
                Point startPoint = _pDragStartedPostion.Value;
                Point nowPoint = MousePosition;

                if (GetDistance(startPoint, nowPoint) >= 10)
                {
                    if (_isDragMouseDown)
                    {
                        _isDragMouseDown = false;
                        _isDragMouseMove = true;
                        DragStarted?.Invoke(this, new DragEventClass(startPoint, SelectedChannel));
                    }
                    DragMoved?.Invoke(this, new DragEventClass(nowPoint, SelectedChannel));
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
                DragEnded?.Invoke(this, new DragEventClass(MousePosition, SelectedChannel));
            }
            _isDragMouseMove = false;
            _pDragStartedPostion = null;
            _isDragMouseDown = false;
        }

        /// <summary>
        /// 셀 우클릭 했을때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMatrixChannelList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
        private void dgvMatrixChannelList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvMatrixChannelList.ReadOnly = true;

            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            this.BeginInvoke(new MethodInvoker(() =>
            {
                dgvMatrixChannelList.Rows[rowIndex].Cells[columnIndex].Selected = true;
            }));

            //CellValueChange?.Invoke(rowIndex, dgvMatrixChannelList.Rows[rowIndex].Cells[columnIndex].Value.ToString());
            CellValueChanged?.Invoke(sender, e);

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

        private void dgvMatrixChannelList_Resize(object sender, EventArgs e)
        {
            UpdateDgvMatrixChannelListLayOut();
        }

        public event EventHandler CellClick;
        public event EventHandler CellValueChanged;
        public event MatrixFrameView.delCellValueChange CellValueChange;

        public event EventHandler<DragEventClass> DragStarted;
        public event EventHandler<DragEventClass> DragMoved;
        public event EventHandler<DragEventClass> DragEnded;



    }
}
