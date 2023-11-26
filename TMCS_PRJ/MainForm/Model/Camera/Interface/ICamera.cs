using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface ICamera :ICameraAction
    {
        event EventHandler CameraSelected;
        event EventHandler CameraSelectedClear;

        int CameraId { get; set; }
        string CameraName { get; set; }

        ICameraAction Protocol { get; set; }

        void ClearCameraSelect();


    }
}


