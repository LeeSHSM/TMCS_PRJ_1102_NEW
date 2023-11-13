using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TMCS_PRJ
{
    public class MainPresenter
    {
        MainView _view;
        MatrixPresenter _matrixControl;
        IProgress<ProgressReport> _progress;

        public MainPresenter(MainView view, IProgress<ProgressReport> progress)
        {
            _progress = progress;
            _view = view;
            
            _matrixControl = new MatrixPresenter(8, 8,progress);
            InitializeViewEvent();

            //나중에 ip관련정보, DB접속정보들은 xml파일로 분리하자...그래야 컴파일 없이 외부에서 수정가능할듯?
            _matrixControl.SetConnectInfo(new RTVDMMatrixToIP(GlobalSetting.MATRIX_IP,GlobalSetting.MATRIX_PORT, progress));
            _matrixControl.SetConnectDBInfo(GlobalSetting.MATRIX_DB);            
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

            _matrixControl.MioFrameDelete += _matrixControl_MioFrameDelete;
        }

        /// <summary>
        /// 비동기로 초기화시작 
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            await _matrixControl.InitializeAsync();

            _matrixControl.StartConnectionAsync(); //서버와 통신... 중요하긴한데 일단 백그라운드 실행
        }



        #region Event Handles

        private void _view_Form_Load(object? sender, EventArgs e)
        {
            _view.InitMatrixFrame(_matrixControl.GetMatrixFrame());
            _view.InitMioFrames(_matrixControl.LoadMioFrames());

            _matrixControl.ChangeMatrixChannelList("INPUT");            
        }

        private void _view_FormClose(object? sender, EventArgs e)
        {
            _matrixControl.SaveMatrixInfo();
        }

        private void _view_btnOutputClick(object? sender, EventArgs e)
        {
            _matrixControl.ChangeMatrixChannelList("OUTPUT");
        }

        private void _view_btnInputClick(object? sender, EventArgs e)
        {
            _matrixControl.ChangeMatrixChannelList("INPUT");
        }

        private void _view_btnAddMioFrameClick(object? sender, EventArgs e)
        {
            _view.AddMioFrame(_matrixControl.AddMatrixInOutFrame());
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
