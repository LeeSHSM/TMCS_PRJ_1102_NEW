using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMCS_PRJ;

namespace LshCamera
{
    public class CameraPresenter
    {
        private CameraManager _cameraManager;
        private CameraType _selectedCamera;
        private CameraControlerView _cameraControler;

        public CameraPresenter()
        {
            _cameraManager = new CameraManager();
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            _cameraControler.CameraMove += _cameraControler_CameraMove;
            _cameraControler.CameraMoveEnded += _cameraControler_CameraMoveEnded;
        }

        private void _cameraControler_CameraMoveEnded(object? sender, EventArgs e)
        {
            
        }

        private void _cameraControler_CameraMove(object? sender, EventArgs e)
        {
            
        }

        public void SetCamera(CameraType camera)
        {
            _cameraManager.AddCamera(camera);
            camera.CameraSelected += Camera_CameraSelected;
        }

        public void SetCameraControler(CameraControlerView cameraControler)
        {
            _cameraControler = cameraControler;
        }

        private void Camera_CameraSelected(object? sender, EventArgs e)
        {
            CameraType camera = sender as CameraType;
            _selectedCamera = camera;
        }

        public void PanTilit(int cameraNum)
        {
            
        }

        public void test(CameraType camera)
        {
            _cameraManager.test(camera);
        }
    }
}
