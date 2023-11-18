using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera2
{
    public interface ICameraType
    {
        int CameraId { get; set; }
        string CameraName { get; set; }

    }
}
