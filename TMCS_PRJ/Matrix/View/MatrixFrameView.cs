using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public interface MatrixFrameView
    {
        string ChannelType { get; set; }
        MatrixChannel SelectedChannel { get;}
        void SetMatrixChannelList(DataTable dataTable);

        public delegate string delChangeMatrixChangeList(string channelType);
        event delChangeMatrixChangeList ChangeMatrixChannelListClick;
        event EventHandler CellClick;
    }
}
