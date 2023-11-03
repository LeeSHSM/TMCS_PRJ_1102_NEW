using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMCS_PRJ
{
    public class MainPresenter
    {
        MainView _view;
        MatrixPresenter _matrixControl;
        List<MatrixInOutSelectFrame> _matrixInOutSelectFrame = new List<MatrixInOutSelectFrame>();


        public MainPresenter(MainView view)
        {
            _view = view;
            _matrixControl = new MatrixPresenter(16, 16);
            //나중에 ip관련정보, DB접속정보들은 xml파일로 분리하자...그래야 컴파일 없이 외부에서 수정가능할듯?
            _matrixControl.SetConnectInfo(new RTVDMMatrixToIP(IPAddress.Parse("192.168.50.8"), 23));
            _matrixControl.SetConnectDBInfo("Server=192.168.50.50;Database=TMCS;User Id=sa;password=tkdgus12#;");
            InitializeViewEvent();
        }

        private void InitializeViewEvent()
        {
            _view.Form_Load += _view_Form_Load;
            _view.btnInputClick += _view_btnInputClick;
            _view.btnOutputClick += _view_btnOutputClick;
            _view.btnConnectClick += _view_btnConnectClick;
            _view.btnCreateClick += _view_btnCreateClick;

        }

        private void _view_btnCreateClick(object? sender, EventArgs e)
        {
           
        }

        private void _view_btnConnectClick(object? sender, EventArgs e)
        {
           
        }

        private void _view_btnOutputClick(object? sender, EventArgs e)
        {
            
        }

        private void _view_btnInputClick(object? sender, EventArgs e)
        {
            
        }

        private void _view_Form_Load(object? sender, EventArgs e)
        {
            _view.pnMatrixFrame = (UserControl)_matrixControl.MatrixFrameControl;
        }
    }
}
