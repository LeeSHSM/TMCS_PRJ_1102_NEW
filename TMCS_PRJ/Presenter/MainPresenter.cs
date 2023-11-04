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


        public MainPresenter(MainView view)
        {
            _view = view;
            _matrixControl = new MatrixPresenter(16, 16);
            //나중에 ip관련정보, DB접속정보들은 xml파일로 분리하자...그래야 컴파일 없이 외부에서 수정가능할듯?
            _matrixControl.SetConnectInfo(new RTVDMMatrixToIP(IPAddress.Parse("192.168.50.8"), 23));
            _matrixControl.SetConnectDBInfo("Server=192.168.50.50;Database=TMCS;User Id=sa;password=tkdgus12#;");
            //_matrixControl.StartConnection();
            
            InitializeViewEvent();
        }

        private void InitializeViewEvent()
        {
            _view.Form_Load += _view_Form_Load;
            _view.btnMatrixInputClick += _view_btnInputClick;
            _view.btnMatrixOutputClick += _view_btnOutputClick;
            _view.btnAddMioFrameClick += _view_btnAddMioFrameClick;

            _matrixControl.DragEnded += _matrixControl_DragEnded;
            _matrixControl.DragMoved += _matrixControl_DragMoved;
            _matrixControl.DragStarted += _matrixControl_DragStarted;
        }

        private void _matrixControl_DragStarted(object? sender, DragEventClass e)
        {
            _view.DragStarted(sender, e);
        }

        private void _matrixControl_DragMoved(object? sender, DragEventClass e)
        {
         _view.DragMove(sender, e);   
        }

        private void _matrixControl_DragEnded(object? sender, DragEventClass e)
        {
            _view.DragEnded(sender, e);
        }

        private void _view_btnAddMioFrameClick(object? sender, EventArgs e)
        {
            _view.pnMatrixInOutSelectFrame = _matrixControl.AddMatrixInOutFrame();
        }

        private void _view_btnOutputClick(object? sender, EventArgs e)
        {
            _matrixControl.ChangeMatrixChannelList("OUTPUT");
        }

        private void _view_btnInputClick(object? sender, EventArgs e)
        {
            _matrixControl.ChangeMatrixChannelList("INPUT");
        }

        private void _view_Form_Load(object? sender, EventArgs e)
        {
            _view.pnMatrixFrame = _matrixControl.GetMatrixFrame();
        }
    }
}
