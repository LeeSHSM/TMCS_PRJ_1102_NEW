using LshMatrix;

namespace LshDlp
{
    internal class DlpManager
    {
        public event EventHandler? DlpInputChannelChanged;
        public event EventHandler? DlpInputChannelValueChanged;

        private DlpGroup _dlpGroup;

        internal DlpManager(int rowCount, int colCount)
        {
            _dlpGroup = new DlpGroup(rowCount,colCount);
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            _dlpGroup.InputChannelChanged += _dlpStruct_InputChannelChanged;
            _dlpGroup.InputChannelValueChanged += _dlpStruct_InputChannelValueChanged;
        }

        private void _dlpStruct_InputChannelValueChanged(object? sender, EventArgs e)
        {
            DlpInputChannelValueChanged?.Invoke(sender, e);
        }

        internal Dlp GetDlp(int dlpId)
        {
            Dlp dlp = new Dlp();
            dlp = _dlpGroup.Dlps.FirstOrDefault(dlp => dlp.DlpId == dlpId);

            if (dlp == null)
            {
                throw new ArgumentException("dlp is Null!!!!!!!!");
            }

            return dlp;
        }

        internal List<Dlp> GetDlpList()
        {
            List<Dlp> dlps = _dlpGroup.Dlps;

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
                Dlp matchingDlp = _dlpGroup.Dlps.FirstOrDefault(x => x.DlpId == dlp.DlpId);
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

        internal DlpGroup GetDlpStruct()
        {
            return _dlpGroup;
        }
    }
}
