namespace TMCS_PRJ
{
    public partial class EquipmentStatusForm : Form
    {
        public EquipmentStatusForm()
        {
            InitializeComponent();
        }

        public void Setlbl(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => lblIp.Text = msg));
            }
            else
            {
                lblIp.Text = msg;
            }
        }

        public void SetMatrixStatus(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => matrixStatus.Text = msg));
            }
            else
            {
                matrixStatus.Text = msg;
            }
        }

        public void SetCameraStatus(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => CameraStatus.Text = msg));
            }
            else
            {
                CameraStatus.Text = msg;
            }
        }

        public void SetCameraInfo(string ip, int port)
        {
            string ipinfo = ip + " / " + port;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => cameraInfo.Text = ipinfo));
            }
            else
            {
                cameraInfo.Text = ipinfo;
            }
        }
    }
}
