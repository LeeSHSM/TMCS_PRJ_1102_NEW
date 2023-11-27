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
    }
}
