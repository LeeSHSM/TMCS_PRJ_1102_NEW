using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MainView
    {
        Form GetMainForm();
        Control GetCollidedControl { get; }


        void InitMioFrames(List<IMioFrame> MioFrames);
        void InitMatrixFrame(UserControl uc);
        void InitDlpFrame(UserControl uc);
        void AddMioFrame(UserControl uc);
        void MioFrameDelete(object sender, EventArgs e);

        event EventHandler FormLoad;
        event EventHandler FormClose;
        event EventHandler btnMatrixInputClick;
        event EventHandler btnMatrixOutputClick;
        event EventHandler btnAddMioFrameClick;
        event EventHandler EquipmentStatusClick;

    }
}
