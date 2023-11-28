using System.Data;

namespace LshMatrix
{
    public interface IMFrame
    {
        Form GetFindForm();
        void SetMatrixFrameChannelList(DataTable dataTable);
        void ClearClickedChannel();

        event EventHandler ClickedChannelChanged;
        event EventHandler ClickedChannelNameChanged;
        event EventHandler MFrameToObjectDragEnded;
    }
}
