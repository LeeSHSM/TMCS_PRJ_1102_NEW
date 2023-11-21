using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using TMCS_PRJ;
using LshMatrix;

namespace LshDlp
{
    internal class DlpManager
    {
        public event EventHandler? DlpInputChannelChanged;
        public event EventHandler? DlpInputChannelValueChanged;

        private DlpStruct _dlpStruct;

        internal DlpManager(DlpStruct dlpStruct)
        {
            _dlpStruct = dlpStruct;
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            _dlpStruct.InputChannelChanged += _dlpStruct_InputChannelChanged;
            _dlpStruct.InputChannelValueChanged += _dlpStruct_InputChannelValueChanged;
        }

        private void _dlpStruct_InputChannelValueChanged(object? sender, EventArgs e)
        {
            DlpInputChannelValueChanged?.Invoke(sender, e);
        }

        internal Dlp GetDlp(int dlpId)
        {
            Dlp dlp = new Dlp();
            foreach (Dlp dlpItem in _dlpStruct.Dlps)
            {
                if(dlpItem.DlpId == dlpId)
                {
                    dlp = dlpItem;
                    break;
                }
            }            

            if(dlp == null)
            {
                throw new ArgumentException("dlp is Null!!!!!!!!");
            }

            return dlp;
        }

        internal List<Dlp> GetDlpList()
        {
            List<Dlp> dlps = _dlpStruct.Dlps;

            return dlps;
        }

        internal void SetDlpMatrixPort(int dlpId, int MatrixPort)
        {
            Dlp dlp = GetDlp(dlpId);
            dlp.MatrixPort = MatrixPort;
        }

        internal void SetDlpInputChannel(int dlpId, MatrixChannel mcInput)
        {
            Dlp dlp = GetDlp(dlpId);
            dlp.InputChannel = mcInput;
        }

        internal void MatchingDlpInputListWithMatrix(List<MatrixChannel> mcs, List<DlpFrameControlInfo> dlps)
        {
            foreach (DlpFrameControlInfo dlp in dlps)
            {
                Dlp matchingDlp = _dlpStruct.Dlps.FirstOrDefault(x => x.DlpId == dlp.DlpId);
                matchingDlp.TileMode = dlp.TileMode;
                matchingDlp.Row = dlp.Row;
                matchingDlp.Col = dlp.Col;
                matchingDlp.MatrixPort = dlp.MatrixPort;

                if (mcs != null)
                {
                    foreach (MatrixChannel mcInput in mcs)
                    {
                        if (dlp.InputChannelPort == mcInput.Port)
                        {
                            matchingDlp.InputChannel = mcInput;
                            break;
                        }
                    }
                }
            }
        }


        private void _dlpStruct_InputChannelChanged(object? sender, EventArgs e)
        {
            DlpInputChannelChanged?.Invoke(sender, e);
        }

        internal DlpStruct GetDlpStruct()
        {
            return _dlpStruct;
        }
    }
}
