using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public class CameraDBManager
    {
        private string _connectString;

        public string ConnectString { get => _connectString; set => _connectString = value; }
    }
}
