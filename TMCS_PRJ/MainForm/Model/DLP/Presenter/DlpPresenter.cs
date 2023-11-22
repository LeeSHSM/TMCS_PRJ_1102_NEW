
using TMCS_PRJ;
using LshGlobalSetting;
using System.Linq.Expressions;
using LshMatrix;

namespace LshDlp
{
    public class DlpPresenter
    {
        public delegate void delDlpRouteChanged(int dlpMatrixPort, MatrixChannel mc);
        public event delDlpRouteChanged? DlpRouteChanged;

        public delegate List<MatrixChannel> delMatrixInfoRequest();
        public event delMatrixInfoRequest? DlpMatrixInfoRequest;

        public event EventHandler DlpClick;

        IProgress<ProgressReport> _progress;

        private IDlpFrame _dlpFrame;
        private DlpManager _dlpManager;
        private DlpFrameFileManager _dlpFrameFileManager;

        private string _dlpChannelType;

        public DlpPresenter(int row, int col, IProgress<ProgressReport> progress) 
        {            
            _dlpManager = new DlpManager(new DlpStruct(row, col));
            _dlpFrameFileManager = new DlpFrameFileManager();
            _dlpFrame = new DlpFrame(_dlpManager.GetDlpStruct());

            _progress = progress;
            InitializeDlpFrame();
            InitializeEvent();
        }

        public async Task InitializeAsync()
        {
            List<DlpFrameControlInfo> dlpsInfo  = await _dlpFrameFileManager.LoadDlpsInfoAsync();
            List<MatrixChannel> matrixChannels = DlpMatrixInfoRequest?.Invoke();            

            if (dlpsInfo.Count > 0)
            {
                _dlpManager.MatchingDlpInputListWithMatrix(matrixChannels,dlpsInfo);
            }
            else
            {
                for (int i = 1; i < 9; i++)
                {
                    _dlpManager.SetDlpMatrixPort(i, i);
                }
            }
            _dlpFrame.UpdateDlpTest();
            _dlpManager.DlpInputChannelChanged += _dlpManager_DlpInputChannelChanged;

        }

        private void InitializeDlpFrame()
        {
            _dlpChannelType = "INPUT";
        }

        private void InitializeEvent()
        {
            _dlpManager.DlpInputChannelValueChanged += _dlpManager_DlpInputChannelValueChanged;
            _dlpFrame.DlpClick += _dlpFrame_DlpClick;
        }

        public void SetDlpChannelType(string channelType)
        {
            if(channelType != "INPUT" && channelType != "OUTPUT")
            {
                throw new Exception($"cahnnelType is only INPUT or OUTPUT");
            }
        }

        public UserControl GetDlpFrame()
        {
            return (UserControl)_dlpFrame;
        }

        public void SetMatrixChannelInDlp(int dlpId, MatrixChannel mcInput)
        {
            _dlpManager.SetDlpInputChannel(dlpId, mcInput);          
        }

        public void SaveDlpFrameInfo()
        {
            _dlpFrameFileManager.SaveDlpsInfo(_dlpManager.GetDlpStruct());
        }

        private void _dlpFrame_DlpClick(object? sender, EventArgs e)
        {
            Dlp dlp = sender as Dlp;
            DlpClick?.Invoke(dlp, EventArgs.Empty);
        }

        private void _dlpManager_DlpInputChannelChanged(object? sender, EventArgs e)
        {
            Dlp dlp = sender as Dlp;
            _dlpFrame.SetDlpFrame("INPUT");

            DlpRouteChanged?.Invoke(dlp.MatrixPort, dlp.InputChannel);
        }

        private void _dlpManager_DlpInputChannelValueChanged(object? sender, EventArgs e)
        {
            _dlpFrame.SetDlpFrame("INPUT");
        }

    }
}
