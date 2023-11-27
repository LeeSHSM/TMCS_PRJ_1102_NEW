using LshMatrix;
using System.Diagnostics;

namespace TMCS_PRJ
{
    public partial class MainForm : Form, IMainForm
    {
        public event EventHandler MFrameLoad;

        public event EventHandler FormLoad;
        public event EventHandler FormClose;
        public event EventHandler btnMatrixInputClick;
        public event EventHandler btnMatrixOutputClick;
        public event EventHandler btnAddMioFrameClick;
        public event EventHandler EquipmentStatusClick;
        public event EventHandler CameraLoad;
        public event EventHandler CameraControlerLoad;

        public MainForm()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(30, 30, 30);
            ucCamera1.Load += Camera_Load;
            ucCamera2.Load += Camera_Load;
            ucCameraControler.Load += CameraControl_Load;
        }

        public Form GetMainForm()
        {
            return this;
        }


        public Control GetCollidedControl
        {
            get
            {
                Point formCoordinates = this.PointToClient(Cursor.Position);

                foreach (Control control in this.Controls)
                {
                    if (control.Bounds.Contains(formCoordinates))
                    {
                        Debug.WriteLine(control.ToString());
                        return control;
                    }
                }

                return null;
            }
        }

        public void InitMatrixFrame(UserControl uc)
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

        public void InitDlpFrame(UserControl uc)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetDlpFrame(uc)));
            }
            else
            {
                SetDlpFrame(uc);
            }
        }

        private void SetDlpFrame(UserControl uc)
        {
            Panel pn = new Panel();
            pn.BackColor = Color.White;
            pnDlpFrame.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            //uc.BackColor = Color.White;
        }

        public void AddMioFrame(UserControl uc)
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

        public void InitMioFrames(List<IMioFrame> MioFrames)
        {
            foreach (MioFrame mioFrame in MioFrames)
            {
                pnMioFrame.Controls.Add(mioFrame);
            }
        }

        private Control FindControlById(string controlId)
        {
            foreach (Control control in this.Controls)
            {
                if (control.Name == controlId)
                {
                    return control;
                }
            }
            return null;
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

        private void SetMatrixFrame(UserControl uc)
        {

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
            FormLoad?.Invoke(sender, e);
        }

        private void btnAddMioFrame_Click(object sender, EventArgs e)
        {
            btnAddMioFrameClick(sender, e);
        }

        public void MioFrameDelete(object sender, EventArgs e)
        {
            MioFrame mioFrame = sender as MioFrame;

            foreach (MioFrame mc in pnMioFrame.Controls)
            {
                if (mc == mioFrame)
                {
                    pnMioFrame.Controls.Remove(mc);
                }
            }
            //int pnMioFrameControlsCount = 0;
            //foreach (MatrixInOutSelectFrame mc in pnMioFrame.Controls)
            //{
            //    int maxColCount = 5;
            //    int maxRowCount = 5;
            //    int width = 20;
            //    int height = 30;

            //    int X = ((mc.Width + width) * ((pnMioFrameControlsCount) % maxColCount));
            //    int Y = ((pnMioFrameControlsCount++ / maxRowCount) * (mc.Height + height));

            //    mc.Location = new Point(X, Y);
            //}
        }
        private void 장비등록정보확인ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EquipmentStatusClick(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormClose?.Invoke(this, e);
        }

        private void mFrame_Load(object sender, EventArgs e)
        {
            MFrameLoad?.Invoke(sender, e);
        }

        private void Camera_Load(object sender, EventArgs e)
        {
            CameraLoad?.Invoke(sender, e);
        }

        private void CameraControl_Load(object sender, EventArgs e)
        {
            CameraControlerLoad?.Invoke(sender, e);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (this.ActiveControl.HasChildren)
            {
                // 유저 컨트롤에 포커스가 있을 때 키 이벤트를 무시                
                return;
            }
            Debug.WriteLine("snfma");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            ucCameraControler.Focus();
        }



        #endregion



        /// <summary>
        /// 화면 깜빡임...티어링 등 금지..
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }

}