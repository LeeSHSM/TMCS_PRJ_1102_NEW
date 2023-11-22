using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface ICameraControler
    {
        event EventHandler CameraMove;
        event EventHandler CameraMoveEnded;
    }
}
