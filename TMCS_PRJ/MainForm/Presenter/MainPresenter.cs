using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using LshCamera;
using LshDlp;
using LshGlobalSetting;
using LshMatrix;


namespace TMCS_PRJ
{
    public class MainPresenter
    {
        IMainForm _view;

        MatrixPresenter _matrixControler;
        MatrixChannel? _selectedMatrixChannel;

        DlpPresenter _dlpPresenter;

        CameraPresenter _cameraPresenter;
        IProgress<ProgressReport> _progress;

        

        public MainPresenter(IMainForm view, IProgress<ProgressReport> progress)
        {
            _progress = progress;
            _view = view;

            _matrixControler = new MatrixPresenter(8, 8, progress);
            _matrixControler.SetDBConnectString(GlobalSetting.DBConnectString);
            _matrixControler.SetMatrixServer(new RTVDMMatrixToIP(GlobalSetting.MATRIX_IP, GlobalSetting.MATRIX_PORT, progress));

            _dlpPresenter = new DlpPresenter(2, 4, progress);

            _cameraPresenter = new CameraPresenter();
            _cameraPresenter.SetDBConnectString(GlobalSetting.DBConnectString);
            _cameraPresenter.SetAmxServer("192.168.50.9", 1234);            

            InitializeViewEvent();
        }

        public async Task InitializeAsync()
        {
            await _matrixControler.InitializeAsync();
            await _dlpPresenter.InitializeAsync();
            await _cameraPresenter.InitializeAsync();

            //_matrixControler.ConnectMatrixAsync(); //서버와 통신... 중요하긴한데 일단 백그라운드 실행
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
            _view.MFrameLoad += _view_MFrameLoad;
            _view.CameraLoad += _view_CameraLoad;
            _view.CameraControlerLoad += _view_CameraControlerLoad;

            _matrixControler.MFrameDragEnded += _matrixControler_MFrameDragEnded;
            _matrixControler.MFrameSelectedChannelChanged += _matrixControl_MatrixSelectedChanged;
            _matrixControler.MioFrameDelete += _matrixControl_MioFrameDelete;

            _dlpPresenter.DlpClick += _dlpPresenter_DlpClick;
            _dlpPresenter.DlpMatrixInfoRequest += _dlpPresenter_DlpMatrixInfoRequest;
            _dlpPresenter.DlpRouteChanged += _dlpPresenter_DlpRouteChanged;

        }

        /// <summary>
        /// 비동기로 초기화시작 
        /// </summary>
        /// <returns></returns>




        private void _view_CameraControlerLoad(object? sender, EventArgs e)
        {
            ICameraControler cameraControler = sender as ICameraControler;

            if (cameraControler != null)
            {
                _cameraPresenter.SetCameraControler(cameraControler);
            }
        }

        private void _view_CameraLoad(object? sender, EventArgs e)
        {
            ICamera camera = sender as ICamera;
            if (camera != null)
            {
                _cameraPresenter.SetCamera(camera);
            }
        }

        private void _view_MFrameLoad(object? sender, EventArgs e)
        {
            UserControl userControl = sender as UserControl;
            _matrixControler.SetMFrame(userControl);
        }

        //dlp에서 매트릭스정보 요청
        private List<MatrixChannel> _dlpPresenter_DlpMatrixInfoRequest()
        {
            List<MatrixChannel> mcs = new List<MatrixChannel>();

            mcs = _matrixControler.GetInputList();

            return mcs;
        }

        //dlp에서 dlp라우트 변경이벤트
        private async void _dlpPresenter_DlpRouteChanged(int dlpPort, MatrixChannel mc)
        {
            await _matrixControler.SetMatrixRouteAsync(dlpPort, mc);
        }

        //dlp에서 dlp클릭함
        private void _dlpPresenter_DlpClick(object? sender, EventArgs e)
        {
            if(_selectedMatrixChannel == null || _selectedMatrixChannel.ChannelType != "INPUT")  
            {
                return;
            }
            Dlp dlp = sender as Dlp;

            _dlpPresenter.SetMatrixChannelInDlp(dlp.DlpId, _selectedMatrixChannel);
            _matrixControler.ClearSelectedMatrixChannel();
        }

        private void _matrixControler_MFrameDragEnded(object? sender, EventArgs e)
        {
            if (_view.GetCollidedControl == null || _selectedMatrixChannel == null)
            {
                _matrixControler.ClearSelectedMatrixChannel();
                return;
            }
            Form mainForm = _view.GetMainForm();
            Point formCoordinates = mainForm.PointToClient(Cursor.Position);

            Control dlpParentControl = FindParentControl(_view.GetCollidedControl, typeof(Dlp));   

            if(dlpParentControl != null && _selectedMatrixChannel.ChannelType == "INPUT")
            {
                foreach(Dlp dlp in dlpParentControl.Controls)
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
            _matrixControler.ClearSelectedMatrixChannel();
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

        #region Event Handles

        private void _view_Form_Load(object? sender, EventArgs e)
        {
            _view.InitMatrixFrame(_matrixControler.GetMFrame());
            _view.InitMioFrames(_matrixControler.InitMioFrames());

            _matrixControler.SetMFrameChannelType("INPUT");

            _view.InitDlpFrame(_dlpPresenter.GetDlpFrame());
        }

        private void _view_FormClose(object? sender, EventArgs e)
        {
            _matrixControler.SaveMatrixInfo();
            _dlpPresenter.SaveDlpFrameInfo();
        }

        private void _view_btnOutputClick(object? sender, EventArgs e)
        {
            _matrixControler.SetMFrameChannelType("OUTPUT");
        }

        private void _view_btnInputClick(object? sender, EventArgs e)
        {
            _matrixControler.SetMFrameChannelType("INPUT");
        }

        private void _view_btnAddMioFrameClick(object? sender, EventArgs e)
        {
            _view.AddMioFrame(_matrixControler.AddMatrixInOutFrame());
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
