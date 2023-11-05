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

        public void MioFrameResizeStarted(object sender, MioFrameResizeEventClass e)
        {

            //Point point = new Point(mouseE.Location.X, mouseE.Location.Y);
            MatrixInOutSelectFrame mioFrame = sender as MatrixInOutSelectFrame;
            //Debug.WriteLine(point);
        }

        public void MioFrameResizeMoved(object sender, MioFrameResizeEventClass e)
        {
            MatrixInOutSelectFrame mioFrame = sender as MatrixInOutSelectFrame;
            this.Invalidate(previousDragRect);
            Rectangle currentDragRect = mioFrame.Bounds;
            this.Invalidate(currentDragRect);
            this.Update();

            switch (e.Position)
            {
                case "»óÇÏ":
                    mioFrame.Size = new Size(mioFrame.Width, e.Location.Y);
                    break;
                case "ÁÂ¿ì":
                    mioFrame.Size = new Size(e.Location.X, mioFrame.Height);
                    break;
                case "ÁÂ»ó":
                    break;
                case "¿ì»ó":
                    break;
                case "ÁÂÇÏ":
                    break;
                case "¿ìÇÏ":
                    mioFrame.Size = new Size(e.Location);
                    break;
            }
        }

        public void MioFrameResizeEnded(object sender, MioFrameResizeEventClass e)
        {

        }

        public void MioFrameDelete(object sender, EventArgs e)
        {
            MatrixInOutSelectFrame mioFrame = sender as MatrixInOutSelectFrame;

            foreach(MatrixInOutSelectFrame mc in pnMioFrame.Controls)
            {
                if(mc == mioFrame)
                {
                    pnMioFrame.Controls.Remove(mc);
                }
            }
            int pnMioFrameControlsCount = 0;
            foreach (MatrixInOutSelectFrame mc in pnMioFrame.Controls)
            {                
                int maxColCount = 5;
                int maxRowCount = 5;
                int width = 20;
                int height = 30;

                int X = ((mc.Width + width) * ((pnMioFrameControlsCount) % maxColCount));
                int Y = ((pnMioFrameControlsCount++ / maxRowCount) * (mc.Height + height));

                mc.Location = new Point(X, Y);


            }
        }



        #endregion

        public event EventHandler Form_Load;
        public event EventHandler btnMatrixInputClick;
        public event EventHandler btnMatrixOutputClick;
        public event EventHandler btnAddMioFrameClick;
        public event EventHandler<DragEventClass> MatrixFrameDragEnded;
    }
}