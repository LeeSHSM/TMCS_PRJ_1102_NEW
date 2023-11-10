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
        void SetMatrixChannelList(DataTable dataTable);
        void ClearClickedCell();

        event EventHandler SelectedCellChanged;
        event delCellValueChange CellValueChange;
        event EventHandler CellValueChanged;
        event EventHandler MFrameToObjectDragEnded;

        delegate void delCellValueChange(int rowNum, string channelName);
        
    }
}
