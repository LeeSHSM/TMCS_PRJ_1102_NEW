using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MainView
    {
        event EventHandler Form_Load;
        event EventHandler btnMatrixInputClick;
        event EventHandler btnMatrixOutputClick;
        event EventHandler btnAddMioFrameClick;
        event EventHandler<DragEventClass> MatrixFrameDragEnded;

        Panel pnMatrixFrame { get; }
        Panel pnMatrixInOutSelectFrame { get; }

        void DockMatrixFrame(UserControl uc);
        void AddMatrixInOutSelectFrame(UserControl uc);

        void DragStarted(object sender, DragEventClass e);
        void DragMove(object sender, DragEventClass e);
        void DragEnded(object sender, DragEventClass e);

    }
}
