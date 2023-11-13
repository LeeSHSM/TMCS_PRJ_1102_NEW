using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MatrixInOutSelectFrameView
    {
        MatrixInOutSelectFrame MioFrame { get; }
        MatrixChannel MatrixChannelInput { get; set; }
        MatrixChannel MatrixChannelOutput { get; set; }

        string ParentId {  get; set; }

        Form GetMainForm();

        Point GetPositionInForm();

        event EventHandler InputClick;
        event EventHandler OutputClick;
        event EventHandler MioFrameDelete;
        event delRouteNoChange RouteNoChange;

        delegate void delRouteNoChange(MatrixChannel mcInput, MatrixChannel mcOutput);
    }
}
