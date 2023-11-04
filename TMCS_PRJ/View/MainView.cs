﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MainView
    {
        event EventHandler Form_Load;
        event EventHandler btnMatrixInputClick;
        event EventHandler btnMatrixOutputClick;
        event EventHandler btnAddMioFrameClick;

        UserControl pnMatrixFrame { set; }
        UserControl pnMatrixInOutSelectFrame { set; }

        void DragStarted(object sender, DragEventClass e);
        void DragMove(object sender, DragEventClass e);
        void DragEnded(object sender, DragEventClass e);

    }
}
