using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    internal class CameraManager
    {
        private List<ICamera> Cameras;

        public CameraManager()
        {
            Cameras = new List<ICamera>();
        }


        internal void AddCamera(ICamera camera)
        {
            Cameras.Add(camera);
        }


        internal void PanTilt(ICamera camera)
        {
            
        }

        public void test(ICamera camera)
        {
            //Debug.WriteLine(camera.PanTilt() + camera.CameraId);
        }

    }


}
