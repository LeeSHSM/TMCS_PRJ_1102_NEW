using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using TMCS_PRJ;

namespace LshDlp
{    public class DlpStruct
    {
        public event EventHandler? InputChannelChanged;
        public event EventHandler? InputChannelValueChanged;

        private List<Dlp> _dlps = new List<Dlp>();
        private int _rowCount;
        private int _colCount;

        public DlpStruct(int rowCount, int colCount)
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
                foreach(var dlp in _dlps)
                {
                    //dlp.InputChannelChanged += Dlp_InputChannelChanged;
                    //dlp.InputChannelValueChanged += Dlp_InputChannelValueChanged;
                }
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
                    dlp.TileMode = 0;
                    dlp.MatrixPort = 0;
                    dlp.InputChannel = new MatrixChannel { ChannelName = "-", ChannelType = "INPUT",  Port = 0, RouteNo = 0};
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


        private int _dlpId;
        private int _tileMode;
        private int _row;
        private int _col;
        private int _matrixPort;
        private MatrixChannel? _inputChannel;

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
                if(_inputChannel != null)
                {
                    _inputChannel.MatrixChannelValueChanged -= _inputChannel_MatrixChannelValueChanged;
                }                
                _inputChannel = value;
                InputChannelChanged?.Invoke(this, EventArgs.Empty);                
                _inputChannel.MatrixChannelValueChanged += _inputChannel_MatrixChannelValueChanged;
            }
        }

        private void _inputChannel_MatrixChannelValueChanged(object? sender, EventArgs e)
        {
            InputChannelValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }


}
