using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LshCamera
{
    public partial class CarmeraControlerFrame : UserControl, ICameraControler
    {
        public event EventHandler CameraMove;
        public event EventHandler CameraMoveEnded;

        public CarmeraControlerFrame()
        {
            InitializeComponent();
            InitializeEvent();
        }

        private System.Windows.Forms.Timer cameraMoveTimer;


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

        Button btnTest;

        private void BtnArrow_MouseDown(object? sender, MouseEventArgs e)
        {
            CameraMove?.Invoke(sender, e);
        }
    }

}
