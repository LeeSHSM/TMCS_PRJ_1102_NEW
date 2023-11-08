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
            _view.MatrixFrameDragEndedRequest += _view_MatrixFrameDragEnded;
            _view.EquipmentStatusClick += _view_EquipmentStatusClick;

            _matrixControl.MFrameDragEnded += _matrixControl_DragEnded;
            _matrixControl.MFrameDragMoved += _matrixControl_DragMoved;
            _matrixControl.MFrameDragStarted += _matrixControl_DragStarted;

            _matrixControl.MioFrameResizeStarted += _matrixControl_MioFrameResizeStarted;
            _matrixControl.MioFrameResizeMoved += _matrixControl_MioFrameResizeMoved;
            _matrixControl.MioFrameResizeEnded += _matrixControl_MioFrameResizeEnded;
            _matrixControl.MioFrameDelete += _matrixControl_MioFrameDelete;
        }

        private void _view_EquipmentStatusClick(object? sender, EventArgs e)
        {
            EquipmentStatusForm equipmentStatusForm = new EquipmentStatusForm();
            equipmentStatusForm.Setlbl(GlobalSetting.MATRIX_IP.ToString());
            equipmentStatusForm.ShowDialog();            
        }

        public async Task InitializeAsync()
        {
            _progress?.Report(new ProgressReport { Message = "매트릭스 초기화 시작" });
            //await Task.Delay(500);
            await _matrixControl.InitializeAsync();
            _progress?.Report(new ProgressReport { Message = "매트릭스 초기화 완료" });
            //await Task.Delay(500);
            //await _matrixControl.StartConnectionAsync();
            _matrixControl.StartConnectionAsync();
        }

        private void _matrixControl_MioFrameDelete(object? sender, EventArgs e)
        {
            _view.MioFrameDelete(sender, e);
        }

        private void _matrixControl_MioFrameResizeEnded(object? sender, MioFrameResizeEventClass e)
        {
            _view.MioFrameResizeEnded(sender, e);
        }

        private void _matrixControl_MioFrameResizeMoved(object? sender, MioFrameResizeEventClass e)
        {
            _view.MioFrameResizeMoved(sender, e);
        }

        private void _matrixControl_MioFrameResizeStarted(object? sender, MioFrameResizeEventClass e)
        {
            MatrixInOutSelectFrame mioFrame = sender as MatrixInOutSelectFrame;
            Debug.WriteLine(mioFrame.Size);
            _view.MioFrameResizeStarted(sender, e);
        }



        #region Event Handles

        private void _view_MatrixFrameDragEnded(object? sender, DragEventClass e)
        {

            #region 임시
            // 라벨을 기준으로 충돌체크
            //Label lbl = sender as Label;
            //if (lbl == null) return; // Early return if the sender is not a Label.

            //Point lblLocationOnForm = ConvertToFormCoordinates(lbl.Parent, lbl);
            //Rectangle lblBounds = new Rectangle(lblLocationOnForm, lbl.Size);

            //Control maxOverlapControl = null; // To keep track of the control with maximum overlap.
            //int maxOverlapArea = 0;

            //foreach (Control con in _view.pnMatrixInOutSelectFrame.Controls)
            //{
            //    Point conLocation = ConvertToFormCoordinates(con.Parent, con);
            //    Rectangle conBounds = new Rectangle(conLocation, con.Size);
            //    if (lblBounds.IntersectsWith(conBounds))
            //    {
            //        // Calculate the area of intersection
            //        Rectangle intersection = Rectangle.Intersect(lblBounds, conBounds);
            //        int overlapArea = intersection.Width * intersection.Height;

            //        // Check if this control has the maximum overlap so far
            //        if (overlapArea > maxOverlapArea)
            //        {
            //            maxOverlapArea = overlapArea;
            //            maxOverlapControl = con;
            //        }
            //    }
            //}

            //// If a control with maximum overlap is found and the overlap area is more than zero, call the method
            //if (maxOverlapControl != null && maxOverlapArea > 0)
            //{
            //    _matrixControl.RequestDragEnded(maxOverlapControl, e);
            //}
            #endregion

            //마우스 포인트를 기준으로 충돌체크
            Point mousePositionOnForm = new Point(e.Location.X, e.Location.Y);

            Control maxOverlapControl = null; // To keep track of the control with maximum overlap.
            int maxOverlapArea = 0; // To keep track of the maximum overlap area.

            foreach (Control con in _view.pnMatrixInOutSelectFrame.Controls)
            {
                Point conLocation = ConvertToFormCoordinates(con.Parent, con);
                Rectangle conBounds = new Rectangle(conLocation, con.Size);
                Rectangle mouseBounds = new Rectangle(mousePositionOnForm, new Size(1, 1)); // Mouse position as a tiny rectangle.

                if (mouseBounds.IntersectsWith(conBounds))
                {
                    // Calculate the area of intersection
                    Rectangle intersection = Rectangle.Intersect(mouseBounds, conBounds);
                    int overlapArea = intersection.Width * intersection.Height;

                    // Check if this control has the maximum overlap so far
                    if (overlapArea > maxOverlapArea)
                    {
                        maxOverlapArea = overlapArea;
                        maxOverlapControl = con;
                    }
                }
            }

            // If a control with maximum overlap is found and the overlap area is more than zero, call the method
            if (maxOverlapControl != null && maxOverlapArea > 0)
            {
                _matrixControl.RequestDragEnded(maxOverlapControl, e);
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
            _view.AddMioFrame(_matrixControl.AddMatrixInOutFrame());
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
