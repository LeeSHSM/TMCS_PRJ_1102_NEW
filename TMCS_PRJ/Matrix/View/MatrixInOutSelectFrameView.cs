using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MatrixInOutSelectFrameView
    {
        MatrixChannel MatrixChannelInput { get; set; }
        MatrixChannel MatrixChannelOutput { get; set; }

        Form GetMainForm();


        Point GetPositionInForm();

        event EventHandler InputClick;
        event EventHandler OutputClick;

        event EventHandler<MioFrameResizeEventClass> MioResizeStarted;
        event EventHandler<MioFrameResizeEventClass> MioResizeMove;
        event EventHandler<MioFrameResizeEventClass> MioResizeFinished;

        event EventHandler MioFrameDelete;

        event delRouteNoChange RouteNoChange;

        delegate void delRouteNoChange(MatrixChannel mcInput, MatrixChannel mcOutput);
    }
}
