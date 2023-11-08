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
    public partial class EquipmentStatusForm : Form
    {
        public EquipmentStatusForm()
        {
            InitializeComponent();
        }

        public void Setlbl(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => lblIp.Text = msg));
            }
            else
            {
                lblIp.Text = msg;
            }
        }
    }
}
