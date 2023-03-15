using Logger;
using News.DAL;
using News.Entity.Base;
using News.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace News.Entity.Websites
{
    public class Globes :  RssFeedExtraction, IWebsite
    {
        
        private LogManager _logger;
        public Globes(LogManager log):base(log)
        {
            _logger = LogInstance;
        }


        public override void FeedExtraction(XmlDocument xmlDoc, Category category)
        {
            try
            {
                if (xmlDoc == null || category == null) return;

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//item");

                if(itemNodes != null )
                {
                    foreach (XmlNode itemNode in itemNodes)
                    {
                        var newArticle = new Article()
                        {
                            title = itemNode.SelectSingleNode("title").InnerText,
                            description = ExtractClearDescriptionFromItem(itemNode),
                            link = itemNode.SelectSingleNode("link").InnerText,
                            image = ExtractImageFromItem(itemNode, nsmgr),
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

        private string ExtractImageFromItem(XmlNode itemNode, XmlNamespaceManager nsmgr)
        {       
            nsmgr.AddNamespace("media", "http://search.yahoo.com/mrss/");

            // Select the media:content element and retrieve its 'url' attribute value
            XmlNode mediaNode = itemNode.SelectSingleNode(".//media:content[@type='image/jpg']", nsmgr);
            return mediaNode?.Attributes["url"]?.Value;
        }

        public override string ExtractClearDescriptionFromItem(XmlNode itemNode)
        {
            string plainText;
            XmlNode descriptionNode = itemNode.SelectSingleNode("description");
            if (descriptionNode != null)
            {
                string description = descriptionNode.InnerText;
                // Regular expressions
                plainText = Regex.Replace(description, @"&#\d+;", string.Empty);
                plainText = Regex.Replace(plainText, @"[^א-תa-zA-Z0-9\s.,״׳""']", string.Empty);
                return plainText.Trim();
            }
            return null;
        }
    }
}
