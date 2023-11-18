using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LshCamera;

namespace TMCS_PRJ
{
    public partial class CarmeraControler : UserControl, CameraControlerView
    {


        public CarmeraControler()
        {
            InitializeComponent();
            InitializeEvent();
        }

        public event EventHandler CameraMove;
        public event EventHandler CameraMoveEnded;

        private void InitializeEvent()
        {
            btnRight.MouseDown += BtnArrow_MouseDown;
            btnLeft.MouseDown += BtnArrow_MouseDown;
            btnUp.MouseDown += BtnArrow_MouseDown;
            btnBot.MouseDown += BtnArrow_MouseDown;

            btnRight.MouseUp += BtnArrow_MouseUp;
            btnLeft.MouseUp += BtnArrow_MouseUp;
            btnUp.MouseUp += BtnArrow_MouseUp;
            btnBot.MouseUp += BtnArrow_MouseUp;
        }

        private void BtnArrow_MouseUp(object? sender, MouseEventArgs e)
        {
            CameraMoveEnded?.Invoke(sender, e);
        }

        private void BtnArrow_MouseDown(object? sender, MouseEventArgs e)
        {
            CameraMove?.Invoke(sender, e);
        }
    }

}
