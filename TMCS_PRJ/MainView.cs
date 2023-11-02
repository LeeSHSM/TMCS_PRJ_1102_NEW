using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MainView
    {
        event EventHandler refreshRequest;
        event EventHandler Form_Load;
        event EventHandler btnInputClick;
        event EventHandler btnOutputClick;
        event EventHandler btnCreateClick;

        UserControl pnMatrixFrame { set; }
        UserControl pnMatrixInOutSelectFrame { set; }
    }
}
