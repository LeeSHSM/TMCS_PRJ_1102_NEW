using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MainView
    {
        event EventHandler FormLoad;
        event EventHandler FormClose;
        event EventHandler btnMatrixInputClick;
        event EventHandler btnMatrixOutputClick;
        event EventHandler btnAddMioFrameClick;
        event EventHandler EquipmentStatusClick;

        Form GetMainForm();

        Control GetCollidedControl { get; }


        Panel pnMatrixFrame { get; }
        Panel pnMatrixInOutSelectFrame { get; }

        void InitMioFrames(List<MatrixInOutSelectFrameView> MioFrames);

        void InitMatrixFrame(UserControl uc);

        void InitDlpFrame(UserControl uc);

        void AddMioFrame(UserControl uc);
        void MioFrameDelete(object sender, EventArgs e);




        string lblUpdate {  get; set; }


    }
}
