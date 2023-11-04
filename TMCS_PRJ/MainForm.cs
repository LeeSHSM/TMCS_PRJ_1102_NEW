using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace TMCS_PRJ
{
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this Panel dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
    public partial class MainForm : Form, MainView
    {
        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            pnMatrixFrame.DoubleBuffered(true);
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


        Label lbl;
        public void DragStarted(object sender,DragEventClass e)
        {
            lbl = new Label();
            this.Controls.Add(lbl);
            Debug.WriteLine(e.Location.ToString());
            lbl.Text = e.Channel.ChannelName;
             lbl.BackColor = Color.Red;
            lbl.BringToFront();
            lbl.Location = this.PointToClient(e.Location);
            
        }

        public void DragMove(object sender, DragEventClass e) 
        { 
            lbl.Location = this.PointToClient(e.Location);
            lbl.Refresh();
            Debug.WriteLine(e.Location.ToString());
        }

        public void DragEnded(object sender, DragEventClass e)
        {
            this.Controls.Remove(lbl);
            lbl?.Dispose();
        }




        #region Event Handles
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

        #endregion

        public event EventHandler Form_Load;
        public event EventHandler btnMatrixInputClick;
        public event EventHandler btnMatrixOutputClick;
        public event EventHandler btnAddMioFrameClick;
    }
}