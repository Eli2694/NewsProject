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

namespace News.Entity
{
    public abstract class RssFeedExtraction : BaseEntity
    {
        private LogManager _logger;
        public RssFeedExtraction(LogManager log) : base(log)
        {
            _logger = LogInstance;
        }

        public virtual void FeedExtraction(XmlDocument xmlDoc, Category category)
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
                        Title = itemNode.SelectSingleNode("title").InnerText.Trim(),
                        Description = ExtractClearDescriptionFromItem(itemNode),
                        ArticleLink = itemNode.SelectSingleNode("link").InnerText.Trim(),
                        Image = ExtractImageFromItem(itemNode),
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

        public virtual string ExtractImageFromItem(XmlNode itemNode)
        {
            XmlNode descriptionNode = itemNode.SelectSingleNode("description");
            if (descriptionNode != null)
            {
                string description = descriptionNode.InnerText;
                // Regular expressions
                Match match = Regex.Match(description, @"<img.+?src=[\""'](.+?)[\""'].*?>");
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }
            return null;
        }

        public virtual string ExtractClearDescriptionFromItem(XmlNode itemNode)
        {
            string plainText;
            XmlNode descriptionNode = itemNode.SelectSingleNode("description");
            if (descriptionNode != null)
            {
                string description = descriptionNode.InnerText;
                // Regular expressions
                plainText = Regex.Replace(description, "<.*?>", string.Empty);
                return plainText.Trim();
            }
            return null;
        }
    }
}
