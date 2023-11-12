using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MatrixFrameView
    {
        string NowChannelType { get; set; }
        MatrixChannel SelectedChannel { get; set; }

        Form GetMainForm();


        void SetMatrixChannelList(DataTable dataTable);
        void ClearClickedCell();

        event EventHandler SelectedCellChanged;
        event EventHandler MatrixChannelNameChanged;
        event EventHandler MFrameToObjectDragEnded;

        
    }
}
