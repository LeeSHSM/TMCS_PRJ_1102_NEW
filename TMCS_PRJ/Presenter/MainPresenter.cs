using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            _view.MatrixFrameDragEnded += _view_MatrixFrameDragEnded;

            _matrixControl.DragEnded += _matrixControl_DragEnded;
            _matrixControl.DragMoved += _matrixControl_DragMoved;
            _matrixControl.DragStarted += _matrixControl_DragStarted;
        }



        #region Event Handles

        private void _view_MatrixFrameDragEnded(object? sender, DragEventClass e)
        {
            //_view.pnMatrixInOutSelectFrame.Controls
            Label lbl = sender as Label;
            Point lblLocationOnForm = ConvertToFormCoordinates(lbl.Parent, lbl);

            foreach (Control con in _view.pnMatrixInOutSelectFrame.Controls)
            {
                Point conLocation = ConvertToFormCoordinates(con.Parent, con);

                Rectangle conBounds = new Rectangle(conLocation, con.Size);
                Rectangle lblBounds = new Rectangle(lblLocationOnForm, lbl.Size);
                if(lblBounds.IntersectsWith(conBounds))
                {
                    //MessageBox.Show("겹침!");
                    _matrixControl.RequestDragEnded(con, e);
                }
            }
        }


        private Point ConvertToFormCoordinates(Control parent, Control child)
        {
            // child 컨트롤의 위치를 부모 컨트롤(예: Panel) 좌표계로 가져옵니다.
            Point childLocationOnParent = child.Location;

            // 부모 컨트롤의 스크린 기준 좌표를 가져옵니다.
            Point parentLocationOnForm = parent.PointToScreen(Point.Empty);

            // child 컨트롤의 스크린 기준 좌표를 계산합니다.
            return new Point(parentLocationOnForm.X + childLocationOnParent.X,
                             parentLocationOnForm.Y + childLocationOnParent.Y);
        }


        private bool IsColliding(Control control1, Control control2)
        {
            // control1의 경계를 나타내는 사각형을 얻습니다.
            Rectangle rect1 = control1.Bounds;

            // control2의 경계를 나타내는 사각형을 얻습니다.
            Rectangle rect2 = control2.Bounds;

            // 두 사각형이 겹치는지 확인합니다.
            return rect1.IntersectsWith(rect2);
        }

        private MatrixChannel mmm;
        private void _matrixControl_DragStarted(object? sender, DragEventClass e)
        {
            _view.DragStarted(sender, e);
            //MatrixChannel mc = sender as MatrixChannel;
            //mmm = mc;
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
            _view.AddMatrixInOutSelectFrame(_matrixControl.AddMatrixInOutFrame());
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
            _view.DockMatrixFrame(_matrixControl.GetMatrixFrame());
        }
        #endregion
    }
}
