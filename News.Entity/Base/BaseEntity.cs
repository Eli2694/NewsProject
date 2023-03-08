using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Entity.Base
{
    public class BaseEntity : BaseNews
    {
        public BaseEntity(LogManager log) : base(log)
        {

        }
    }
}
