using LshCamera;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMCS_PRJ
{
    public partial class SamsungCamera : UserControl, CameraType
    {
        public SamsungCamera()
        {
            InitializeComponent();
            lblName.MouseDown += LblName_MouseDown;
        }

        public event EventHandler CameraSelected;

        private void LblName_MouseDown(object? sender, MouseEventArgs e)
        {
            OnMouseDown(e);
            CameraSelected?.Invoke(this, e);
        }

        public int CameraId { get; set; }
        public int InputPort { get; set; }
        public string CameraName { get; set; }
        public int OutputPort { get; set; }

        public string PanTilt()
        {
            string PanTiltMsg = "삼성!!!!";

            return PanTiltMsg;
        }

        public string ZoomOut()
        {
            string ZoomOutMsg = string.Empty;

            return ZoomOutMsg;
        }
    }
}
