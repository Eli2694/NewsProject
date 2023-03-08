using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Entity.Base
{
    public class BaseNews
    {
        public BaseNews(LogManager Log)
        { 
            LogInstance = Log;
        }
        public LogManager LogInstance { get; set; }
    }
}
