using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class MioFrameResizeEventClass
    {
        public Point Location { get; set; }
        public string Position { get; set; }

        public MioFrameResizeEventClass(Point location, string position)
        {
            Location = location;
            Position = position;            
        }

        enum MioFramePosition
        {
            상하,
            좌우,
            좌상,
            우상,
            우하,
            좌하,
        } 
    }
}
