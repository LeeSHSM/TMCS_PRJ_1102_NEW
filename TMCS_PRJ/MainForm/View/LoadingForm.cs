namespace TMCS_PRJ
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            lblInfo.Parent = pictureBox1;
            lblInfo.BackColor = Color.Transparent;
        }

        public void Setlbl(string msg)
        {
            lblInfo.Text = msg;
        }
    }
}
