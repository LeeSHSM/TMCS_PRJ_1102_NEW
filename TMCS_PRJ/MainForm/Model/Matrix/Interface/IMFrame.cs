using System.Data;

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
