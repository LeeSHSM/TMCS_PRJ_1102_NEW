using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshGlobalSetting
{
    public class MioFrameControlInfo
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public DockStyle DockStyle { get; set; }
        public int inputPort {  get; set; }
        public int outputPort { get; set; }
    }
}
