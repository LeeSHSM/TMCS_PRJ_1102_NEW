namespace LshCamera
{
    public interface ICameraControler
    {
        delegate void delCameraPanTilt(int panSpeed, int tiltSpeed, int panDir, int tiltDir);
        event delCameraPanTilt CameraPanTilt;

        delegate void delPresetValueChanged(int presetid, string presetName);
        event delPresetValueChanged PresetNameChanged;

        event EventHandler PresetSave;
        event EventHandler PresetLoad;

        void SelectCamera(ICamera camera);
        void EndKeyEvent();

    }
}
