using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    internal class CameraManager
    {
        private CameraAmxServer _amxServer;
        private CameraDBManager _dbManager;

        List<ICamera> _cameras;
        NetworkStream _amxStream;

        public CameraManager()
        {
            _cameras = new List<ICamera>();
            _dbManager = new CameraDBManager();
        }

        internal void AddCamera(ICamera camera)
        {
            if (camera.Protocol is Visca cameraAction)
            {
                if (_amxStream == null)
                {
                    cameraAction.SetCameraId(camera.CameraId);
                }
                else
                {
                    cameraAction.SetCameraId(camera.CameraId);
                    cameraAction.SetAmxServer(_amxStream);
                }                
            }
            _cameras.Add(camera);
        }

        public void SetAmxServer(CameraAmxServer amxServer)
        {
            _amxServer = amxServer;
            _amxServer.AmxConnected += _amxServer_AmxConnected;
            _amxServer.ConnectAmxServerAsync();
        }

        private void _amxServer_AmxConnected(object? sender, EventArgs e)
        {
            _amxStream = _amxServer.GetStream();
            foreach (ICamera camera in _cameras)
            {
                if (camera.Protocol is Visca cameraAction && camera.Protocol == null)
                {
                    cameraAction.SetAmxServer(_amxStream);
                }
            }
        }

        public void SetIpCamera(NetworkStream amxStream)
        {

        }

        public void CameraPanTilt(ICamera camera,int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            camera.PanTilt(panSpeed, tiltSpeed, panDir, tiltDir);
        }

        public void testBtn(ICamera camera)
        {
            byte[] command = new byte[] { 0x81, 0x01, 0x06, 0x02, 0x18, 0x18, 0x00, 0x08, 0x0a, 0x05, 0x08, 0x04, 0x09, 0x03, 0x0e, 0xFF };

            _amxStream.Write(command, 0, command.Length);
        }

        public async Task SavePreeset(ICamera camera, int preesetNum)
        {
            if(camera == null)
            {
                return;
            }
            camera.SavePreset();
            byte[] tat = await _amxServer.GetCameraPosition();            
            _dbManager.SavePreset(camera, preesetNum, tat);
            string tmp = BitConverter.ToString(tat, 0, 9);
            //Debug.WriteLine(BitConverter.ToString(buffer, 0, bytesRead));
            Debug.WriteLine(tmp);
        }

        public void LoadPreeset(ICamera camera, int preesetNum) 
        {            
            byte[] tat = _dbManager.GetPreset(camera, preesetNum);
            if(tat == null)
            {
                return;
            }

            //_amxStream.Write(newArray, 0, newArray.Length);
            camera.LoadPreset(tat);

        }



    }


}
