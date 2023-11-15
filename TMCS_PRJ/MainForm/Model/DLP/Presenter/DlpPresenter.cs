using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMCS_PRJ;
using static TMCS_PRJ.GlobalSetting;

namespace LshDlp
{
    public class DlpPresenter
    {
        public delegate void delDlpRouteChanged(int dlpMatrixPort, MatrixChannel mc);
        public event delDlpRouteChanged? DlpRouteChanged;

        IProgress<ProgressReport> _progress;

        private DlpFrameView _dlpFrame;
        private DlpManager _dlpManager;

        private string _dlpChannelType;

        public DlpPresenter(int row, int col, IProgress<ProgressReport> progress) 
        {            
            _dlpManager = new DlpManager(new DlpStruct(row, col));
            _dlpFrame = new DlpFrame(_dlpManager.GetDlpStruct());
            _progress = progress;
            InitializeDlpFrame();
            InitializeEvent();

            for(int i = 1; i < 9; i++)
            {
                _dlpManager.SetDlpMatrixPort(i, i);
            }

        }

        private void InitializeDlpFrame()
        {
            _dlpChannelType = "INPUT";
        }

        private void InitializeEvent()
        {
            _dlpManager.DlpInputChannelChanged += _dlpManager_DlpInputChannelChanged;
            _dlpManager.DlpInputChannelValueChanged += _dlpManager_DlpInputChannelValueChanged;
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
