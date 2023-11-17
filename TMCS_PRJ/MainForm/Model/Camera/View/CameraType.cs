using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface CameraType
    {
        event EventHandler CameraSelected;

        int CameraId { get; set; }
        int InputPort {  get; set; }
        string CameraName {  get; set; }
        int OutputPort {  get; set; }

        string PanTilt();
        string ZoomOut();
        

    }

}
