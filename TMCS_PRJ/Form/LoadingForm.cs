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
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            lblInfo.Parent = pictureBox1;
            lblInfo.BackColor = Color.Transparent;
        }

        public void Setlbl(string msg)
        {
            lblInfo.Text = msg;
        }
    }
}
