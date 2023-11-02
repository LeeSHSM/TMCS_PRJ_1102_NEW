using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MatrixInOutSelectFrameView
    {
        MatrixChannel MatrixChannelInput { get; set; }
        MatrixChannel MatrixChannelOutput { get; set; }

        void SetMatrixOutputChannel(MatrixChannel matrixChannel);
        void SetMatrixInputChannel(MatrixChannel matrixChannel);
        event EventHandler matrixChannelInputClick;
        event EventHandler matrixChannelOutputClick;
    }
}
