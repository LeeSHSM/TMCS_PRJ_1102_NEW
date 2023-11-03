using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMCS_PRJ
{
    public partial class MatrixInOutSelectFrame : UserControl, MatrixInOutSelectFrameView
    {
        public MatrixInOutSelectFrame()
        {
            InitializeComponent();
            lblOutput.Click += Input_Click;
            lblInput.Click += Output_Click;
        }

        private MatrixChannel _matrixChannelOutput;
        private MatrixChannel _matrixChannelInput;

        public MatrixChannel MatrixChannelOutput
        {
            get { return _matrixChannelOutput; }
            set
            {
                _matrixChannelOutput = value;
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateMatrixChannel(_matrixChannelOutput)));
                }
                else
                {
                    UpdateMatrixChannel(_matrixChannelOutput);
                }
            }
        }
        public MatrixChannel MatrixChannelInput
        {
            get { return _matrixChannelInput; }
            set
            {
                _matrixChannelInput = value;
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateMatrixChannel(_matrixChannelInput)));
                }
                else
                {
                    UpdateMatrixChannel(_matrixChannelInput);
                }

                if(_matrixChannelOutput.Port > 0)
                {
                    RouteNoChanege?.Invoke(_matrixChannelInput.Port, _matrixChannelOutput.Port);
                }                
            }
        }

        private void UpdateMatrixChannel(MatrixChannel mc)
        {           
            if (mc == _matrixChannelOutput)
            {
                lblOutput.Text = mc.ChannelName;
            }
            else if (mc == _matrixChannelInput)
            {
                lblInput.Text = mc.ChannelName;
            }
        }

        private void Output_Click(object? sender, EventArgs e)
        {
            OutputClick?.Invoke(this, e);
        }

        private void Input_Click(object? sender, EventArgs e)
        {
            InputClick?.Invoke(this, e);
        }

        public event EventHandler InputClick;
        public event EventHandler OutputClick;
        public event MatrixInOutSelectFrameView.delRouteNoChange RouteNoChanege;
    }
}
