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
        List<ICamera> _cameras;

        public CameraManager()
        {
            _cameras = new List<ICamera>();
        }

        internal void AddCamera(ICamera camera)
        {
            if(camera.Protocol is Visca cameraAction)
            {                
                cameraAction.SetIPAddress("192.168.50.8", 10000);
            }
            _cameras.Add(camera);
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

        public void PanRight(ICamera camera)
        {

        }

        public void PanLeft(ICamera camera)
        {

        }

        public void TilitUp(ICamera camera)
        {

        }

        public void TiliDown(ICamera camera)
        {

        }

        public void ZoomIn(ICamera camera)
        {

        }

        public void ZoomOut(ICamera camera)
        {

        }

        public void SavePreeset(ICamera camera, int preesetNum)
        {

        }

        public void LoadPreeset(ICamera camera, int preesetNum) 
        { 
        
        }



    }


}
