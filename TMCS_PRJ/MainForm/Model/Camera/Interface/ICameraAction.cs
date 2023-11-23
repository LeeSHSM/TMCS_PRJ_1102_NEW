using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface ICameraAction
    {

        void SetCameraId(int CameraId);


        void PanStart(int speed, int Direction);
        void PanStop();
        void TiltStart(int speed, int Direction);
        void TiltStop();
        void PanTilt(int PanSpeed, int tiltSpeed, int panDirection, int tiltDirection);
        void PanTiltStop();



        //void ZoomStart();
        //void ZoomStop();
    }

    public class Visca : ICameraAction
    {
        string AMX_serverIp;
        int AMX_serverPort;

        int _cameraId;

        public void SetIPAddress(string serverIp, int serverPort)
        {
            AMX_serverIp = serverIp;
            AMX_serverPort = serverPort;
        }

        public void PanStart(int speed, int Direction)
        {
            
        }

        public void PanStop()
        {
            throw new NotImplementedException();
        }

        public void PanTilt(int PanSpeed, int tiltSpeed, int panDirection, int tiltDirection)
        {
            byte bytePanSpeed = (byte)PanSpeed;
            byte bytetiltSpeed = (byte)tiltSpeed;
            byte bytepanDirection = (byte)panDirection;
            byte bytetiltDirection = (byte)tiltDirection;
            byte[] command = new byte[] { 0x81, 0x06, 0x01, bytePanSpeed, bytetiltSpeed, bytepanDirection, bytetiltDirection, 0xFF };

            throw new NotImplementedException();
        }

        public void PanTiltStop()
        {
            throw new NotImplementedException();
        }

        public void SetCameraId(int CameraId)
        {
            _cameraId = CameraId;
        }

        public void TiltStart(int speed, int Direction)
        {
            throw new NotImplementedException();
        }

        public void TiltStop()
        {
            throw new NotImplementedException();
        }
    }
}
