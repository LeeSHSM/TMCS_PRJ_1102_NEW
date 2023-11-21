using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshMatrix
{    public interface IMioFrame
    {
        MatrixChannel MatrixChannelInput { get; set; }
        MatrixChannel MatrixChannelOutput { get; set; }

        Form GetMainForm();

        Point GetPositionInForm();

        event EventHandler InputClick;
        event EventHandler OutputClick;
        event EventHandler MioFrameDelete;
        event delRouteNoChange RouteNoChange;

        delegate void delRouteNoChange(MatrixChannel mcInput, MatrixChannel mcOutput);
    }
}
