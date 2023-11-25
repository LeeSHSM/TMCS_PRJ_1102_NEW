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
        List<ICamera> _cameras;
        NetworkStream AmxStream;

        public CameraManager()
        {
            _cameras = new List<ICamera>();
        }

        internal void AddCamera(ICamera camera)
        {
            if(camera.Protocol is Visca cameraAction)
            {
                cameraAction.SetCameraId(camera.CameraId);
                cameraAction.SetAmxServer(AmxStream);
            }
            _cameras.Add(camera);
        }

        public void SetAmxServer(NetworkStream amxStream)
        {
            AmxStream = amxStream;
        }

        public bool IsDuplicateCameraId(ICamera camera)
        {            
            foreach (ICamera c in _cameras)
            {
                if (c.CameraId == camera.CameraId) 
                {
                    return true;
                }
            }
            return false;
        }

        public void CameraPanTilt(ICamera camera,int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            camera.PanTilt(panSpeed, tiltSpeed, panDir, tiltDir);
        }

        public void testBtn(ICamera camera)
        {
            byte[] command = new byte[] { 0x81, 0x01, 0x06, 0x02, 0x18, 0x18, 0x00, 0x08, 0x0a, 0x05, 0x08, 0x04, 0x09, 0x03, 0x0e, 0xFF };


            AmxStream.Write(command, 0, command.Length);
        }

        public void SavePreeset(ICamera camera, int preesetNum)
        {

        }

        public void LoadPreeset(ICamera camera, int preesetNum) 
        { 
        
        }



    }


}
