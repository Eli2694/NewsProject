using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using News.DAL;
using News.Entity.Base;
using News.Model;
using System.Net.Http;
using System.Xml;
using System.ServiceModel.Syndication;
using News.Entity.Websites;

namespace News.Entity
{
    public class ManageRssFeeds : BaseEntity
    {
        private LogManager _logger;
        private IEnumerable<Category>? _categories;
        private IWebsite _website;

        public ManageRssFeeds(LogManager log) : base(log)
        {
            _logger = LogInstance; // LogInstance from BaseNews

            InitFeeds();
        }

        private void InitFeeds()
        {
            try
            {

                _categories = DataLayer.Data.CategoryRepository.GetAll();

                if( _categories != null )
                {
                    foreach (var category in _categories)
                    {
                        GetXmlData(category);
                    }
                
                }
                else
                {
                    _logger.AddLogItemToQueue("Can't find categories", null, "Error");
                }

                
            }
            catch (Exception exc)
            {

                _logger.AddLogItemToQueue(exc.Message, exc, "Exception");
            }
            
        }

        private async Task GetXmlData(Category category)
        {
            using (var client = new HttpClient())

            {

                // Make a GET request to the URL 

                var response = await client.GetAsync(category.URL);

                // Ensure the response was successful 

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException exc)
                {

                    _logger.AddLogItemToQueue($"Unseccefull GET request of: {category.Name},{category.Source}", exc, "Error");
                }

                // Read the content of the response 

                var content = await response.Content.ReadAsStringAsync();

                // Output the content of the response 

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                WebsiteCoordination(doc, category);
            }
        }

        private void WebsiteCoordination(XmlDocument doc,Category category)
        {
            switch (category.Source) 
            {
                case "globes":
                    _website = new Globes(_logger);
                    break;
                case "ynet":
                    _website = new Ynet(_logger);
                    break;
                case "maariv":
                    _website = new Maariv(_logger);
                    break;
                case "walla":
                   _website = new Walla(_logger);
                    break;
                default:
                    _logger.AddLogItemToQueue("Category source was not found", null, "Error");
                    break;
            }

            _website.FeedExtraction(doc, category);
        }


    }
}
