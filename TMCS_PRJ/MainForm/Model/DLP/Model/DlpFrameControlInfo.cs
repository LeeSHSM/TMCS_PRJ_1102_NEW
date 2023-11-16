using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMCS_PRJ;

namespace LshDlp
{
    public class DlpFrameControlInfo
    {
        public DlpFrameControlInfo() { }

        public DlpFrameControlInfo(int dlpId, int tileMode, int row, int col, int matrixPort, int inputChannelPort) 
        {
            DlpId = dlpId;
            TileMode = tileMode;
            Row = row;
            Col = col;
            MatrixPort = matrixPort;
            InputChannelPort = inputChannelPort;
        }

        public int DlpId;
        public int TileMode;
        public int Row;
        public int Col;
        public int MatrixPort;
        public int InputChannelPort;
    }
}
