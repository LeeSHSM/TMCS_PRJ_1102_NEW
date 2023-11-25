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

		public async Task InitializeAsync()
		{
			
		}

		public void SetAmxServer(string amxIp, int amxPort)
		{
			_cameraAmxServer = new CameraAmxServer(amxIp, amxPort);
            _cameraManager.SetAmxServer(_cameraAmxServer);
        }

		public void SetDBConnectString(string connectString)
		{
			
		}

		public void SetCamera(ICamera camera)
		{
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
            _cameraControler.SavePreset += _cameraControler_SavePreset;
            _cameraControler.LoadPreset += _cameraControler_LoadPreset;
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
			if(_selectedCamera == null)
			{
				return;
			}

			if (_selectedCamera.Protocol is Visca)
			{
				if ((!_cameraAmxServer.GetStatus()))
				{
					await _cameraAmxServer.ConnectAmxServerAsync();
				}
				else
				{
					_cameraManager.CameraPanTilt(_selectedCamera, panSpeed, tiltSpeed, panDir, tiltDir);
				}
			}
			else if(_selectedCamera.Protocol is IpCamera)
			{

			}

			
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

			//_cameraControler.SelectedCamera();
			
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
