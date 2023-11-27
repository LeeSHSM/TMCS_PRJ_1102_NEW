using System.Diagnostics;
using System.Net.Sockets;

namespace LshCamera
{
    public class CameraPresenter
    {
        private CameraManager _cameraManager;
        private CameraDBManager _cameraDBManager;
        private CameraAmxServer _cameraAmxServer;
        private CameraIpManager _cameraIpManager;
        private ICamera? _selectedCamera;
        private ICameraControler _cameraControler;

        private NetworkStream? _amxStream;

        public CameraPresenter()
        {
            _cameraManager = new CameraManager();
            _cameraDBManager = new CameraDBManager();
        }

        public void SetDBConnectString(string connectString)
        {
            _cameraDBManager.SetDBConectionString(connectString);
            _cameraManager.SetDBServer(_cameraDBManager);
        }

        public void SetAmxServer(string amxIp, int amxPort)
        {
            _cameraAmxServer = new CameraAmxServer(amxIp, amxPort);
            _cameraManager.SetAmxServer(_cameraAmxServer);
        }

        public async Task InitializeAsync()
        {

        }

        public async Task SetCameraAsync(ICamera camera)
        {
            camera.Protocol = new Visca();            
            camera.CameraSelected += Camera_CameraSelected;
            camera.CameraSelectedClear += Camera_CameraSelectedClear;
            await _cameraManager.SetCameraAsync(camera);
        }

        private void Camera_CameraSelectedClear(object? sender, EventArgs e)
        {
            _cameraControler.EndKeyEvent();
            _selectedCamera = null;
        }

        public void SetCameraControler(ICameraControler cameraControler)
        {
            _cameraControler = cameraControler;
            _cameraControler.CameraPanTilt += _cameraControler_CameraPanTilt;
            _cameraControler.PresetSave += _cameraControler_SavePreset;
            _cameraControler.PresetLoad += _cameraControler_LoadPreset;
            _cameraControler.PresetNameChanged += _cameraControler_PresetNameChanged;
        }

        private async void _cameraControler_PresetNameChanged(int presetid, string presetName)
        {
            if(_selectedCamera  == null)
            {
                return;
            }

            await _cameraManager.ChangePresetName(_selectedCamera,presetid,presetName);
        }

        private void _cameraControler_LoadPreset(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            _cameraManager.LoadPreeset(_selectedCamera, (int)btn.Tag);
        }

        private void _cameraControler_SavePreset(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            _cameraManager.SavePreeset(_selectedCamera, (int)btn.Tag);
        }

        private async void _cameraControler_CameraPanTilt(int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            if (_selectedCamera == null)
            {
                return;
            }

            await _cameraManager.CameraPanTilt(_selectedCamera, panSpeed, tiltSpeed, panDir, tiltDir);
        }

        private void Camera_CameraSelected(object? sender, EventArgs e)
        {
            if (_selectedCamera != null)
            {
                _selectedCamera.ClearCameraSelect();
            }

            ICamera camera = sender as ICamera;
            _selectedCamera = camera;
            _cameraControler.SelectCamera(camera);
            Debug.WriteLine(camera.CameraName);
        }

        public void ClearCameraSelect()
        {
            if (_selectedCamera != null)
            {
                _cameraControler.EndKeyEvent();
                _selectedCamera.ClearCameraSelect();
                _selectedCamera = null;
            }
        }

        public void PanTilit(int cameraNum)
        {

        }

    }
}
