using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logger.ILogger;

namespace Logger
{
    internal class LogConsole : ILog
    {
        public void Init()
        {

        }
        public void Log(LogItem item)
        {
            string log;

            if (item.ExceptionSource == null)
            {
                log = item.Type + " - " + item.DateTime + " - " + item.Message;
            }
            else
            {
                log = item.Type + " - " + item.DateTime + " - " + item.ExceptionSource?.StackTrace?.ToString() + ", " + item.Message;
            }

            Console.WriteLine(log);

        }

        public void LogCheckHoseKeeping()
        {

        }
    }
}
