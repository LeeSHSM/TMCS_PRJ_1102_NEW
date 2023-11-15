using System.Diagnostics;
using LshDlp;


namespace TMCS_PRJ
{
    public class MainPresenter
    {
        MainView _view;

        MatrixPresenter _matrixPresenter;
        MatrixChannel? _selectedMatrixChannel;

        DlpPresenter _dlpPresenter;
        IProgress<ProgressReport> _progress;

        public MainPresenter(MainView view, IProgress<ProgressReport> progress)
        {
            _progress = progress;
            _view = view;
            
            _matrixPresenter = new MatrixPresenter(8, 8,progress);            

            //나중에 ip관련정보, DB접속정보들은 xml파일로 분리하자...그래야 컴파일 없이 외부에서 수정가능할듯?
            _matrixPresenter.SetConnectInfo(new RTVDMMatrixToIP(GlobalSetting.MATRIX_IP,GlobalSetting.MATRIX_PORT, progress));
            _matrixPresenter.SetConnectDBInfo(GlobalSetting.MATRIX_DB);

            _dlpPresenter = new DlpPresenter(2,4, progress);

            InitializeViewEvent();
        }

        /// <summary>
        /// 이벤트 초기화  
        /// </summary>
        private void InitializeViewEvent()
        {
            _view.FormLoad += _view_Form_Load;
            _view.FormClose += _view_FormClose;
            _view.btnMatrixInputClick += _view_btnInputClick;
            _view.btnMatrixOutputClick += _view_btnOutputClick;
            _view.btnAddMioFrameClick += _view_btnAddMioFrameClick;
            _view.EquipmentStatusClick += _view_EquipmentStatusClick;

            _matrixPresenter.MioFrameDelete += _matrixControl_MioFrameDelete;
            _matrixPresenter.MatrixSelectedChanged += _matrixControl_MatrixSelectedChanged;
            _matrixPresenter.DragEnded += _matrixPresenter_DragEnded;

            _dlpPresenter.DlpRouteChanged += _dlpPresenter_DlpRouteChanged;
        }

        private async void _dlpPresenter_DlpRouteChanged(int dlpPort, MatrixChannel mc)
        {
            await _matrixPresenter.SetMatrixRouteAsync(dlpPort, mc);
        }

        private void _matrixPresenter_DragEnded(object? sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            Form mainForm = _view.GetMainForm();
            Point formCoordinates = mainForm.PointToClient(Cursor.Position);

            Debug.WriteLine(formCoordinates);

            Control useControl = FindParentControl(_view.GetCollidedControl, typeof(Dlp));

            if(useControl != null)
            {
                foreach(Dlp dlp in useControl.Controls)
                {
                    Point screenCoordinates = dlp.PointToScreen(Point.Empty);

                    // 스크린 좌표를 'mainForm'의 좌표계로 변환
                    Point formRelativeCoordinates = mainForm.PointToClient(screenCoordinates);

                    Rectangle rectDlp = new Rectangle(formRelativeCoordinates, dlp.Size);
                    Rectangle rectMouse = new Rectangle(formCoordinates, new Size(0,0));

                    if (rectDlp.IntersectsWith(rectMouse))
                    {
                        Debug.WriteLine(dlp.DlpId);
                        _dlpPresenter.SetMatrixChannelInDlp(dlp.DlpId, _selectedMatrixChannel);
                        break;
                    }
                }
            }
        }

        private Control FindParentControl(Control control, Type senderType)
        {
            foreach (Control childControl in control.Controls)
            {
                if(childControl == null)
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

        private void _matrixControl_MatrixSelectedChanged(object? sender, EventArgs e)
        {
            MatrixChannel mc = sender as MatrixChannel;
            _selectedMatrixChannel = mc;
        }

        /// <summary>
        /// 비동기로 초기화시작 
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            await _matrixPresenter.InitializeAsync();

            _matrixPresenter.StartConnectionAsync(); //서버와 통신... 중요하긴한데 일단 백그라운드 실행
        }



        #region Event Handles

        private void _view_Form_Load(object? sender, EventArgs e)
        {
            _view.InitMatrixFrame(_matrixPresenter.InitMatrixFrame());
            _view.InitMioFrames(_matrixPresenter.InitMioFrames());

            _matrixPresenter.SetMFrameChannelType("INPUT");

            _view.InitDlpFrame(_dlpPresenter.GetDlpFrame());
        }

        private void _view_FormClose(object? sender, EventArgs e)
        {
            _matrixPresenter.SaveMatrixInfo();
        }

        private void _view_btnOutputClick(object? sender, EventArgs e)
        {
            _matrixPresenter.SetMFrameChannelType("OUTPUT");
        }

        private void _view_btnInputClick(object? sender, EventArgs e)
        {
            _matrixPresenter.SetMFrameChannelType("INPUT");
        }

        private void _view_btnAddMioFrameClick(object? sender, EventArgs e)
        {
            _view.AddMioFrame(_matrixPresenter.AddMatrixInOutFrame());
        }

        private void _matrixControl_MioFrameDelete(object? sender, EventArgs e)
        {
            _view.MioFrameDelete(sender, e);
        }

        private void _view_EquipmentStatusClick(object? sender, EventArgs e)
        {
            EquipmentStatusForm equipmentStatusForm = new EquipmentStatusForm();
            equipmentStatusForm.Setlbl(GlobalSetting.MATRIX_IP.ToString());
            equipmentStatusForm.ShowDialog();
        }

        #endregion
    }
}
