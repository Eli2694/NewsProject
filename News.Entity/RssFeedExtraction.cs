using Logger;
using News.DAL;
using News.Entity.Base;
using News.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace News.Entity
{
    public abstract class RssFeedExtraction : BaseEntity
    {
        public static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

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
                        Link = itemNode.SelectSingleNode("link").InnerText.Trim(),
                        Image = ExtractImageFromItem(itemNode),
                        CreatedDate = itemNode.SelectSingleNode("pubDate").InnerText.Trim(),
                        CategoryID = category.Id,
                        Guid = $"{itemNode.SelectSingleNode("title").InnerText}:{itemNode.SelectSingleNode("pubDate").InnerText}",
                        ArticleClicks = 0
                    };

                    // Add to database

                    _semaphore.Wait();
                    try
                    {
                        if (DataLayer.Data.Article.Any(article => article.Guid == newArticle.Guid))
                        {
                            // article already exists, skip insertion
                            return;
                        }

                        // article does not exist, insert it
                        DataLayer.Data.ArticleRepository.Insert(newArticle);
                    }
                    finally
                    {
                        _semaphore.Release();
                    }

                }
            }
            catch (SqlException ex)
            {
                _logger.AddLogItemToQueue(ex.Message, ex, "Exception");

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
