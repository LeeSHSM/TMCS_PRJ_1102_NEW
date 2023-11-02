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
            mcInput.Click += McInput_Click;
            mcOutput.Click += McOutput_Click;
        }

        public MatrixChannel MatrixChannelInput
        {
            get { return mcInput; }
            set
            {
                mcInput = value;
                Debug.WriteLine(mcInput.Text);
            }
        }

        public MatrixChannel MatrixChannelOutput
        {
            get { return mcOutput; }
            set
            {
                mcOutput = value;
                Debug.WriteLine(mcOutput.Text);
            }
        }


        public void SetMatrixInputChannel(MatrixChannel matrixChannel)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateMatrixChannel(mcInput, matrixChannel)));
            }
            else
            {
                UpdateMatrixChannel(mcInput, matrixChannel);
            }
        }

        public void SetMatrixOutputChannel(MatrixChannel matrixChannel)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateMatrixChannel(mcOutput, matrixChannel)));
            }
            else
            {
                UpdateMatrixChannel(mcOutput, matrixChannel);
            }
        }

        private void McOutput_Click(object? sender, EventArgs e)
        {
            matrixChannelOutputClick?.Invoke(this, e);
        }

        private void McInput_Click(object? sender, EventArgs e)
        {
            matrixChannelInputClick?.Invoke(this, e);
        }

        private void UpdateMatrixChannel(MatrixChannel oldMc, MatrixChannel newMc)
        {
            oldMc.ChannelName = newMc.ChannelName;
            oldMc.Text = oldMc.ChannelName;
            oldMc.ChannelType = newMc.ChannelType;
            oldMc.Port = newMc.Port;

            if (oldMc.ChannelType == "INPUT")
            {
                mcOutput.RouteNo = newMc.RouteNo;
            }
        }


        public event EventHandler matrixChannelInputClick;
        public event EventHandler matrixChannelOutputClick;
    }
}
