using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface ICamera
    {
        int CameraId { get; set; }
        string CameraName { get; set; }
    }
}


