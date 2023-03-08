using News.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace News.Entity.Websites
{
    public interface IWebsite
    {
        void FeedExtraction(XmlDocument xmlDoc, Category category);

    }
}
