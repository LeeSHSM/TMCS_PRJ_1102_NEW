using LshMatrix;
using System.Runtime.Intrinsics.Arm;

namespace LshDlp
{
    public class DlpGroup
    {
        public event EventHandler? InputChannelChanged;
        public event EventHandler? InputChannelValueChanged;

        private List<Dlp> _dlps = new List<Dlp>();
        private int _rowCount;
        private int _colCount;

        public DlpGroup(int rowCount, int colCount)
        {
            RowCount = rowCount;
            ColCount = colCount;
            InitializeDlp();
        }

        internal int RowCount { get => _rowCount; set => _rowCount = value; }
        internal int ColCount { get => _colCount; set => _colCount = value; }
        internal List<Dlp> Dlps
        {
            get => _dlps;
            set
            {
                _dlps = value;
            }
        }

        private void InitializeDlp()
        {
            int dlpCount = 1;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    Dlp dlp = new Dlp();
                    dlp.DlpId = dlpCount++;
                    dlp.Row = i;
                    dlp.Col = j;                                    
                    dlp.InputChannelChanged += Dlp_InputChannelChanged;
                    dlp.InputChannelValueChanged += Dlp_InputChannelValueChanged;
                    Dlps.Add(dlp);
                }
            }
        }

        private void Dlp_InputChannelValueChanged(object? sender, EventArgs e)
        {
            InputChannelValueChanged?.Invoke(sender, e);
        }

        private void Dlp_InputChannelChanged(object? sender, EventArgs e)
        {
            InputChannelChanged?.Invoke(sender, e);
        }
    }

    public class Dlp : Label
    {
        public event EventHandler? InputChannelChanged;
        public event EventHandler? InputChannelValueChanged;
        public Dlp() 
        {
            this.Font = new Font("맑은 고딕", 10, FontStyle.Regular);
            this._inputChannel = new MatrixChannel { ChannelName = "-", ChannelType = "INPUT", Port = 0, RouteNo = 0 };
        }

        private int _dlpId;
        private int _tileMode;
        private int _row;
        private int _col;
        private int _matrixPort;
        private MatrixChannel _inputChannel;

        public int TileMode
        {
            get => _tileMode;
            set
            {
                _tileMode = value;
            }
        }
        public int Row { get => _row; set => _row = value; }
        public int Col { get => _col; set => _col = value; }
        public int MatrixPort { get => _matrixPort; set => _matrixPort = value; }
        public int DlpId { get => _dlpId; set => _dlpId = value; }
        public MatrixChannel InputChannel
        {
            get => _inputChannel;
            set
            {
                _inputChannel.MatrixChannelValueChanged -= _inputChannel_MatrixChannelValueChanged;
                _inputChannel = value;
                _inputChannel.MatrixChannelValueChanged += _inputChannel_MatrixChannelValueChanged;
                this.Text = _inputChannel.ChannelName;                
                InputChannelChanged?.Invoke(this, EventArgs.Empty);                
            }
        }

        private void _inputChannel_MatrixChannelValueChanged(object? sender, EventArgs e)
        {         
            this.Text = _inputChannel.ChannelName;
            InputChannelValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }


}
