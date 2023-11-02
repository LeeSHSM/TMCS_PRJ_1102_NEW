using System.Data.SqlClient;
using System.Drawing.Drawing2D;

namespace TMCS_PRJ
{
    public partial class MainForm : Form, MainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public UserControl pnMatrixInOutSelectFrame { set => throw new NotImplementedException(); }
        UserControl MainView.pnMatrixFrame
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => SetMatrixControl(value)));
                }
                else
                {
                    SetMatrixControl(value);
                }
            }
        }

        private void SetMatrixControl(UserControl value)
        {
            pnMatrixFrame.Controls.Add(value);
            value.Dock = DockStyle.Fill;
        }

        private void btnMatrixInput_Click(object sender, EventArgs e)
        {
            btnInputClick?.Invoke(sender, e);
        }

        private void btnMatrixOutput_Click(object sender, EventArgs e)
        {
            btnOutputClick?.Invoke(sender, e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Form_Load?.Invoke(sender, e);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnectClick(sender, e);
        }

        private void bbbb_Click(object sender, EventArgs e)
        {
            btnCreateClick(sender, e);
        }

        public event EventHandler refreshRequest;
        public event EventHandler Form_Load;
        public event EventHandler btnInputClick;
        public event EventHandler btnOutputClick;
        public event EventHandler btnCreateClick;
        public event EventHandler btnConnectClick;
    }
}