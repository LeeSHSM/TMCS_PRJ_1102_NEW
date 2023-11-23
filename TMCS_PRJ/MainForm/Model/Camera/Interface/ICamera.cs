using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface ICamera 
    {
        event EventHandler CameraSelected;

        int CameraId { get; set; }
        string CameraName { get; set; }

        ICameraAction Protocol { get; set; }

        void SetCameraActionConnectInfo(string serverIp, int serverPort);

        void PanStart(int speed, int Direction);
        void PanStop();
        void TiltStart(int speed, int Direction);
        void TiltStop();
        void PanTilt(int PanSpeed, int tiltSpeed, int panDirection, int tiltDirection);
        void PanTiltStop();



    }
}


