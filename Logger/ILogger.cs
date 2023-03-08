using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class ILogger
    {
        public interface ILog
        {
            void Init();
            void Log(LogItem item);
            void LogCheckHoseKeeping();
        }
    }
}
