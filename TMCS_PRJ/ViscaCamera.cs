using LshCamera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    internal class ViscaCamera : ICamera
    {
        public int CameraId { get; set; }
        public string CameraName { get; set; }

        private ICameraAction ActionHandler;

        public ViscaCamera(int cameraId, string cameraName, ICameraAction actionHandler)
        {
            CameraId = cameraId;
            CameraName = cameraName;
            ActionHandler = actionHandler;
        }

        public async Task PanAsync()
        {
            await ActionHandler.PanAsync(CameraId);
        }

        public async Task TiltAsync()
        {
            await ActionHandler.TiltAsync();
        }

        public async Task ZoomInAsync()
        {
            await ActionHandler.ZoomInAsync();
        }

        public async Task ZoomOutAsync()
        {
            await ActionHandler.ZoomOutAsync();
        }


    }
}
