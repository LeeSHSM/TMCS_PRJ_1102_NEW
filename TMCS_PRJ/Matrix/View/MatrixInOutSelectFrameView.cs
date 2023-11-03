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

        event EventHandler InputClick;
        event EventHandler OutputClick;
        event delRouteNoChange RouteNoChange;

        delegate void delRouteNoChange(int inputNo, int outputNo);
    }
}
