using Logger;
using News.DAL;
using News.Entity.Base;
using News.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
                        title = itemNode.SelectSingleNode("title").InnerText.Trim(),
                        description = ExtractClearDescriptionFromItem(itemNode),
                        link = itemNode.SelectSingleNode("link").InnerText.Trim(),
                        image = ExtractImageFromItem(itemNode),
                        createdDate = ExtractDateTimeFitSQL(itemNode.SelectSingleNode("pubDate").InnerText.Trim()),
                        categoryID = category.id,
                        guid = $"{itemNode.SelectSingleNode("title").InnerText}:{itemNode.SelectSingleNode("pubDate").InnerText}",
                        articleClicks = 0
                    };        

                    // Add to database
                    _semaphore.Wait();
                    try
                    {
                        if (DataLayer.Data.Article.Any(article => article.guid == newArticle.guid))
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

        public virtual string ExtractDateTimeFitSQL(string pubDate)
        {
            string[] formats = {
                    "ddd, dd MMM yyyy HH:mm:ss zzz",
                    "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                    "ddd, dd MMM yyyy HH:mm:ss"
            };

            DateTime result;
            if (DateTime.TryParseExact(pubDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result.ToString("yyyy-MM-dd HH:mm:ss");
            }

            Console.WriteLine("invalid format");
            return "invalid format";
        }

    }
}
