using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public class CameraPreset
    {
        private int _cameraid;
        private Dictionary<int, byte[]> _preset;

        public int Cameraid { get => _cameraid; set => _cameraid = value; }
        public Dictionary<int, byte[]> Preset { get => _preset; set => _preset = value; }
    }
}
