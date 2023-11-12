﻿using System;
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

        public event Action init;

        public void lblUpodate(string msg)
        {
            _view.lblUpdate = msg;
        }


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

        private void InitializeViewEvent()
        {
            _view.FormLoad += _view_Form_Load;
            _view.btnMatrixInputClick += _view_btnInputClick;
            _view.btnMatrixOutputClick += _view_btnOutputClick;
            _view.btnAddMioFrameClick += _view_btnAddMioFrameClick;
            _view.EquipmentStatusClick += _view_EquipmentStatusClick;

            _matrixControl.MioFrameDelete += _matrixControl_MioFrameDelete;
        }

        public async Task InitializeAsync()
        {
            _progress?.Report(new ProgressReport { Message = "매트릭스 초기화 시작" });
            await _matrixControl.InitializeAsync();
            _progress?.Report(new ProgressReport { Message = "매트릭스 초기화 완료" });

            _matrixControl.StartConnectionAsync();
        }

        private void _view_EquipmentStatusClick(object? sender, EventArgs e)
        {
            EquipmentStatusForm equipmentStatusForm = new EquipmentStatusForm();
            equipmentStatusForm.Setlbl(GlobalSetting.MATRIX_IP.ToString());
            equipmentStatusForm.ShowDialog();            
        }





        #region Event Handles

        private void _view_Form_Load(object? sender, EventArgs e)
        {
            _view.DockMatrixFrame(_matrixControl.GetMatrixFrame());
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

        #endregion
    }
}
