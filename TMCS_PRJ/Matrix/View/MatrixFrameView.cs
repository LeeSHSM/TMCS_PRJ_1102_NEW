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
        string ChannelType { get; set; }
        MatrixChannel SelectedChannel { get;}
        void SetMatrixChannelList(DataTable dataTable);
        void ClearClickedCell();

        event EventHandler CellClick;
        event EventHandler<DragEventClass> DragStarted;
        event EventHandler<DragEventClass> DragMoved;
        event EventHandler<DragEventClass> DragEnded;
        
    }
}
