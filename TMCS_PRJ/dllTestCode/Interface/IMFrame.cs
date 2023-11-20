using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshMatrix
{
    public interface IMFrame
    {
        Form GetFindForm();
        void SetMatrixFrameChannelList(DataTable dataTable);
        void ClearClickedCell();

        event EventHandler SelectedCellChanged;
        event EventHandler MatrixChannelNameChanged;
        event EventHandler MFrameToObjectDragEnded;

    }
}
