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
        private List<CameraType> Cameras;

        public CameraManager()
        {
            Cameras = new List<CameraType>();
        }


        internal void AddCamera(CameraType camera)
        {
            Cameras.Add(camera);
        }


        internal void PanTilt(CameraType camera)
        {
            
        }

        public void test(CameraType camera)
        {
            Debug.WriteLine(camera.PanTilt() + camera.CameraId);
        }

    }


}
