using LshMatrix;

namespace TMCS_PRJ
{
    public interface IMainForm
    {
        Form GetMainForm();
        Control GetCollidedControl { get; }

        void InitMioFrames(List<IMioFrame> MioFrames);

        void InitMatrixFrame(UserControl uc);
        void InitDlpFrame(UserControl uc);
        void AddMioFrame(UserControl uc);
        void MioFrameDelete(object sender, EventArgs e);

        event EventHandler MFrameLoad;

        event EventHandler FormLoad;
        event EventHandler FormClose;
        event EventHandler btnMatrixInputClick;
        event EventHandler btnMatrixOutputClick;
        event EventHandler btnAddMioFrameClick;
        event EventHandler EquipmentStatusClick;

        event EventHandler CameraLoad;
        event EventHandler CameraControlerLoad;

    }
}
