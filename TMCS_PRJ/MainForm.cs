using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace TMCS_PRJ
{
    public partial class MainForm : Form, MainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Panel MainView.pnMatrixInOutSelectFrame
        {
            get { return pnMioFrame; }

        }

        Panel MainView.pnMatrixFrame
        {
            get { return pnMatrixFrame; }
        }



        public void DockMatrixFrame(UserControl uc)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetMatrixFrame(uc)));
            }
            else
            {
                SetMatrixFrame(uc);
            }

        }

        public void AddMatrixInOutSelectFrame(UserControl uc)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetMatrixMioFrame(uc)));
            }
            else
            {
                SetMatrixMioFrame(uc);
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

        private Rectangle previousDragRect = Rectangle.Empty;

        public void DragStarted(object sender, DragEventClass e)
        {
            lbl = new Label();
            lbl.Size = new Size(50, 50);

            Point point = new Point(e.Location.X - (lbl.Width / 2), e.Location.Y - (lbl.Height / 2));

            lbl.Location = this.PointToClient(point);
            this.Controls.Add(lbl);
            lbl.Text = e.Channel.ChannelName;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.BackColor = Color.Red;
            lbl.BringToFront();
        }

        public void DragMove(object sender, DragEventClass e)
        {
            this.Invalidate(previousDragRect);
            Rectangle currentDragRect = lbl.Bounds;
            this.Invalidate(currentDragRect);
            this.Update();

            previousDragRect = currentDragRect;

            Point point = new Point(e.Location.X - (lbl.Width / 2), e.Location.Y - (lbl.Height / 2));
            lbl.Location = this.PointToClient(point);

            Debug.WriteLine(e.Location.ToString());
        }

        public void DragEnded(object sender, DragEventClass e)
        {
            MatrixFrameDragEnded(lbl, e);
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
        public event EventHandler<DragEventClass> MatrixFrameDragEnded;
    }
}