using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MainView
    {
        event EventHandler FormLoad;
        event EventHandler btnMatrixInputClick;
        event EventHandler btnMatrixOutputClick;
        event EventHandler btnAddMioFrameClick;
        event EventHandler<DragEventClass> MatrixFrameDragEndedRequest;
        event EventHandler EquipmentStatusClick;

        Panel pnMatrixFrame { get; }
        Panel pnMatrixInOutSelectFrame { get; }

        void DockMatrixFrame(UserControl uc);


        void DragStarted(object sender, DragEventClass e);
        void DragMove(object sender, DragEventClass e);
        void DragEnded(object sender, DragEventClass e);

        void MioFrameResizeStarted(object sender, MioFrameResizeEventClass e);
        void MioFrameResizeMoved(object sender, MioFrameResizeEventClass e);
        void MioFrameResizeEnded(object sender, MioFrameResizeEventClass e);

        void AddMioFrame(UserControl uc);
        void MioFrameDelete(object sender, EventArgs e);




        string lblUpdate {  get; set; }


    }
}
