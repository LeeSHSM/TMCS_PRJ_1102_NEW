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
        private ICamera _selectedCamera;
        private ICameraControler _cameraControler;
        private List<ICamera> _cameraTypes;

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
            
        }

        private void _cameraControler_CameraMove(object? sender, EventArgs e)
        {
            
        }

        public void SetCamera(ICamera camera)
        {
            _cameraManager.AddCamera(camera);
            //camera.CameraSelected += Camera_CameraSelected;
        }

        public void SetCameraControler(ICameraControler cameraControler)
        {
            _cameraControler = cameraControler;
        }

        private void Camera_CameraSelected(object? sender, EventArgs e)
        {
            ICamera camera = sender as ICamera;
            _selectedCamera = camera;
        }

        public void PanTilit(int cameraNum)
        {
            
        }

        public void test(ICamera camera)
        {
            _cameraManager.test(camera);
        }
    }
}
