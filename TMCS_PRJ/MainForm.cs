using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace TMCS_PRJ
{
    public partial class MainForm : Form, MainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        UserControl MainView.pnMatrixInOutSelectFrame
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => SetMatrixMioFrame(value)));
                }
                else
                {
                    SetMatrixMioFrame(value);
                }
            }
        }

        UserControl MainView.pnMatrixFrame
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => SetMatrixFrame(value)));
                }
                else
                {
                    SetMatrixFrame(value);
                }
            }
        }
        private void SetMatrixMioFrame(UserControl value)
        {
            int pnMioFrameControlsCount = pnMioFrame.Controls.Count;
            int maxColCount = 5;
            int maxRowCount = 5;
            int width = 20;
            int height = 30;
            value.Size = new Size(100, 120);

            int X = ((value.Width + width) * ((pnMioFrameControlsCount) % maxColCount));
            int Y = ((pnMioFrameControlsCount / maxRowCount) * (value.Height + height));

            pnMioFrame.Controls.Add(value);
            value.Location = new Point(X, Y);

        }

        private void SetMatrixFrame(UserControl value)
        {
            pnMatrixFrame.Controls.Add(value);
            value.Dock = DockStyle.Fill;
        }

        private void btnMatrixInput_Click(object sender, EventArgs e)
        {
            btnMatrixInputClick?.Invoke(sender, e);
        }

        private void btnMatrixOutput_Click(object sender, EventArgs e)
        {
            btnMatrixOutputClick?.Invoke(sender, e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Form_Load?.Invoke(sender, e);
        }

        private void btnAddMioFrame_Click(object sender, EventArgs e)
        {
            btnAddMioFrameClick(sender, e);
        }

        public event EventHandler Form_Load;
        public event EventHandler btnMatrixInputClick;
        public event EventHandler btnMatrixOutputClick;
        public event EventHandler btnAddMioFrameClick;
    }
}