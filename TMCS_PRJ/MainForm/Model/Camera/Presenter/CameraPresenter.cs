using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMCS_PRJ;

namespace LshCamera
{
    public class CameraPresenter
    {
        private CameraManager _cameraManager;
        private ICamera _selectedCamera;
        private ICameraControler _cameraControler;

        public CameraPresenter()
        {
            _cameraManager = new CameraManager();
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            //_cameraControler.CameraMove += _cameraControler_CameraMove;
            //_cameraControler.CameraMoveEnded += _cameraControler_CameraMoveEnded;
        }

        private void _cameraControler_CameraMoveEnded(object? sender, EventArgs e)
        {
            Debug.WriteLine("멈춘다..");
        }

        private void _cameraControler_CameraMove(object? sender, EventArgs e)
        {
            if (_selectedCamera == null)
            {
                return;
            }
            _cameraManager.PanRight(_selectedCamera);
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
        }

        
        public void SetCameraControler(ICameraControler cameraControler)
        {
            _cameraControler = cameraControler;
            _cameraControler.CameraMove += _cameraControler_CameraMove;
            _cameraControler.CameraMoveEnded += _cameraControler_CameraMoveEnded;
        }

        private void Camera_CameraSelected(object? sender, EventArgs e)
        {
            ICamera camera = sender as ICamera;
            _selectedCamera = camera;
            Debug.WriteLine(camera.CameraName);
        }

        public void PanTilit(int cameraNum)
        {
            
        }

    }
}
