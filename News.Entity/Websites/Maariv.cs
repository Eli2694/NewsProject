using Logger;
using News.DAL;
using News.Entity.Base;
using News.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace News.Entity.Websites
{
    public class Maariv : BaseEntity, IWebsite
    {
        private LogManager _logger;
        public Maariv(LogManager log) : base(log)
        {
            _logger = LogInstance;
        }

        public void FeedExtraction(XmlDocument xmlDoc, Category category)
        {
            try
            {
                if (xmlDoc == null || category == null) return;

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//item");

                foreach (XmlNode itemNode in itemNodes)
                {
                    var newArticle = new Article()
                    {
                        Title = itemNode.SelectSingleNode("title").InnerText,
                        Description = itemNode.SelectSingleNode("description").InnerText,
                        ArticleLink = itemNode.SelectSingleNode("link").InnerText,
                        Image = ExtractImageFromItem(itemNode, nsmgr),
                        CategoryID = category.Id,
                        Source = category.Source,
                        Guid = $"{itemNode.SelectSingleNode("title").InnerText}:{itemNode.SelectSingleNode("pubDate").InnerText}",
                        ArticleClicks = 0
                    };

                    // Add to database

                    if (!DataLayer.Data.Article.Any(article => article.Guid == newArticle.Guid))
                    {
                        DataLayer.Data.ArticleRepository.Insert(newArticle);
                    }

                }
            }
            catch (ArgumentException ex)
            {

                _logger.AddLogItemToQueue(ex.Message, ex, "Exception");
            }
            catch (Exception ex)
            {

                _logger.AddLogItemToQueue(ex.Message, ex, "Exception");
            }

        }

        private string ExtractImageFromItem(XmlNode itemNode, XmlNamespaceManager nsmgr)
        {
            nsmgr.AddNamespace("media", "http://search.yahoo.com/mrss/");

            // Select the media:content element and retrieve its 'url' attribute value
            XmlNode mediaNode = itemNode.SelectSingleNode(".//media:content[@type='image/jpg']", nsmgr);
            return mediaNode?.Attributes["url"]?.Value;
        }
    }
}
