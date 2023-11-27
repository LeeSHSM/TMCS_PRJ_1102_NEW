namespace LshCamera
{
    public interface ICamera 
    {
        event EventHandler CameraSelected;
        event EventHandler CameraSelectedClear;

        int CameraId { get; set; }
        string CameraName { get; set; }

        CameraPresetGroup PresetGroup { get; set; }

        ICameraAction Protocol { get; set; }



        void SetCameraId(int CameraId);

        Task PanTiltAsync(int panSpeed, int tiltSpeed, int panDir, int tiltDir);

        Task<byte[]> SavePresetAsync();

        Task LoadPresetAsync(int presetNum);

        void ClearCameraSelect();


    }
}


