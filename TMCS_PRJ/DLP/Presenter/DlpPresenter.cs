using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class DlpPresenter
    {
        IProgress<ProgressReport> _progress;

        private DlpFrameView _dlpFrame;
        private DlpManager _dlpManager;
        public DlpPresenter(int row, int col, IProgress<ProgressReport> progress) 
        {
            _dlpFrame = new DlpFrame();
            _dlpManager = new DlpManager(new DlpStruct(row, col));

            _progress = progress;
        }

        public UserControl GetDlpFrame()
        {
            _dlpFrame.SetDlpFrame(_dlpManager.GetDlpStruct());
            return (UserControl)_dlpFrame;
        }
    }
}
