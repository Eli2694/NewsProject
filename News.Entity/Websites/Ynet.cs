using Logger;
using News.DAL;
using News.Entity.Base;
using News.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace News.Entity.Websites
{
    public class Ynet : RssFeedExtraction, IWebsite
    {
        
        public Ynet(LogManager log) : base(log)
        {
            
        }
       
    }
}
