using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface ICameraControler
    {
        delegate void delCameraPanTilt(int panSpeed, int tiltSpeed, int panDir, int tiltDir);
        event delCameraPanTilt CameraPanTilt;

        event EventHandler SavePreset;
        event EventHandler LoadPreset;

        void SelectedCamera(ICamera camera);
        void EndKeyEvent();
        
        event EventHandler testBtn;
    }
}
