using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class LogItem
    {
        public string Type { get; set; } // warning,event,exception
        public Exception ExceptionSource { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
