using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMCS_PRJ;

namespace LshCamera
{
    public class CameraPresenter
    {
        private CameraManager _cameraManager;
        private CameraDBManager _cameraDBManager;
        private ICamera? _selectedCamera;
        private ICameraControler _cameraControler;

        public CameraPresenter()
        {
            _cameraManager = new CameraManager();
            _cameraDBManager = new CameraDBManager();
        }

        public async Task InitializeAsync()
        {

        }

        public void SetAmxServer(NetworkStream amxStream)
        {
            _cameraManager.SetAmxServer(amxStream);
        }

        public void SetDBServer()
        {
            
        }




        public void SetCamera(ICamera camera)
        {
            if(_cameraManager.IsDuplicateCameraId(camera))
            {
                Debug.WriteLine("중복됨!!");
            }
            camera.Protocol = new Visca();
            _cameraManager.AddCamera(camera);
            camera.CameraSelected += Camera_CameraSelected;
            camera.CameraSelectedClear += Camera_CameraSelectedClear;
        }

        private void Camera_CameraSelectedClear(object? sender, EventArgs e)
        {
            _cameraControler.EndKeyEvent();
            _selectedCamera = null;
        }

        public void SetCameraControler(ICameraControler cameraControler)
        {
            _cameraControler = cameraControler;
            _cameraControler.testBtn += _cameraControler_testBtn;
            _cameraControler.CameraPanTilt += _cameraControler_CameraPanTilt;


        }

        private void _cameraControler_CameraPanTilt(int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            if(_selectedCamera == null)
            {
                return;
            }

            _cameraManager.CameraPanTilt(_selectedCamera,panSpeed, tiltSpeed, panDir, tiltDir);
        }

        private void _cameraControler_testBtn(object? sender, EventArgs e)
        {
            _cameraManager.testBtn(_selectedCamera);
        }      

        private void Camera_CameraSelected(object? sender, EventArgs e)
        {
            if(_selectedCamera != null)
            {
                _selectedCamera.ClearCameraSelect();
            }

            _cameraControler.SelectedCamera();
            
            ICamera camera = sender as ICamera;
            _selectedCamera = camera;
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
