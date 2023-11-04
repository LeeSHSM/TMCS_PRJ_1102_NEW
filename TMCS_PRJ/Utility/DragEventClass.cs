using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class DragEventClass
    {
        public Point Location { get; set; }
        public MatrixChannel Channel { get; set; }

        public DragEventClass(Point location, MatrixChannel channel)
        {
            Location = location;
            Channel = channel;
        }
    }
}
