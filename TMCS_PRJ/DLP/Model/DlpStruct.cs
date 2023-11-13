using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{    public class DlpStruct
    {
        private List<Dlp> _dlps = new List<Dlp>();
        private int _rowCount;
        private int _colCount;

        public DlpStruct(int rowCount, int colCount)
        {
            RowCount = rowCount;
            ColCount = colCount;
            InitializeDlp();
        }

        public int RowCount { get => _rowCount; set => _rowCount = value; }
        public int ColCount { get => _colCount; set => _colCount = value; }
        public List<Dlp> Dlps { get => _dlps; set => _dlps = value; }

        private void InitializeDlp()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    Dlp dlp = new Dlp();
                    dlp.Row = i;
                    dlp.Col = j;
                    dlp.TileMode = 0;
                    dlp.MatrixPort = 0;
                    Dlps.Add(dlp);
                }
            }
        }
    }

    public class Dlp : Label
    {
        public event EventHandler TileModeChanged;

        private int _tileMode;
        private int _row;
        private int _col;
        private int _matrixPort;

        public int TileMode 
        { 
            get => _tileMode;
            set 
            { 
                _tileMode = value; 
                TileModeChanged?.Invoke(this, EventArgs.Empty);            
            }
        }
        public int Row { get => _row; set => _row = value; }
        public int Col { get => _col; set => _col = value; }
        public int MatrixPort { get => _matrixPort; set => _matrixPort = value; }
    }


}
