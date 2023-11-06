using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class ProgressReport
    {
        private string _message;
        private string _test;
        public string Message 
        {
            get { return _message; }
            set 
            { 
                _message = value;        
            }
        }

        public string Test { get => _test; set => _test = value; }
    }
}
