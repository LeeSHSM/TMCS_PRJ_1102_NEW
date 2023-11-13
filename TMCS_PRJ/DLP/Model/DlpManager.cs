using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class DlpManager
    {
        private DlpStruct _dlpStruct;
        public DlpManager(DlpStruct dlpStruct)
        {
            _dlpStruct = dlpStruct;
        }

        public DlpStruct GetDlpStruct()
        {
            return _dlpStruct;
        }
    }
}
