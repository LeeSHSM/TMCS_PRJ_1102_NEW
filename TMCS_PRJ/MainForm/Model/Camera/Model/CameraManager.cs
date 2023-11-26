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
        //NetworkStream _amxStream;

        public CameraManager()
        {
            _cameras = new List<ICamera>();
            _dbManager = new CameraDBManager();
        }

        internal void AddCamera(ICamera camera)
        {
            if (camera.Protocol is Visca cameraAction)
            {
                cameraAction.SetCameraId(camera.CameraId);
                cameraAction.SetAmxServer(_amxServer);
            }
            _cameras.Add(camera);
        }
       
        public void SetAmxServer(CameraAmxServer amxServer)
        {
            _amxServer = amxServer;
            _amxServer.AmxConnected += _amxServer_AmxConnected;
        }
        
        private void _amxServer_AmxConnected(object? sender, EventArgs e)
        {
            foreach (ICamera camera in _cameras)
            {
                if (camera.Protocol is Visca cameraAction && camera.Protocol == null)
                {
                    cameraAction.SetAmxServer(_amxServer);
                }
            }
        }

        public async Task CameraPanTilt(ICamera camera,int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {            
            await camera.PanTiltAsync(panSpeed, tiltSpeed, panDir, tiltDir);
        }

        public async Task SavePreeset(ICamera camera, int preesetNum)
        {
            if(camera == null)
            {
                return;
            }
            byte[] tat = await camera.SavePresetAsync();
            //byte[] tat = await _amxServer.GetCameraPosition();            
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
            camera.LoadPresetAsync(tat);

        }



    }


}
