using Logger;
using News.DAL;
using News.Entity.Base;
using News.Model;
using System.Xml;
using News.Entity.Websites;

namespace News.Entity
{

    public class ManageRssFeeds : BaseEntity
    {
        private LogManager _logger;
        private IEnumerable<Category>? _categories;
        private IWebsite _website;
        private readonly DataLayer _dataLayer;

        //public Queue<XmlDocument> _xmlContentQueue;
        //Task queueTask = null;

        public ManageRssFeeds(DataLayer dataLayer, LogManager log) : base(log)
        {
            _logger = LogInstance; // LogInstance from BaseNews
            _dataLayer = dataLayer;
            Task.Run(InitFeeds);
            
        }

        private void InitFeeds()
        {

            _logger.AddLogItemToQueue("Get Rss Feeds And Send Http Request For Information About News", null, "Event");

            try
            {
                while (true) 
                {
                    _categories = _dataLayer.CategoryRepository.GetAll();

                    if (_categories != null)
                    {
                        foreach (var category in _categories)
                        {
                            GetXmlData(category);
                        }

                        Thread.Sleep(1000 * 60 * 60);

                    }
                    else
                    {
                        _logger.AddLogItemToQueue("Can't find categories", null, "Error");
                    }
                }
                
            }
            catch (Exception ex)
            {

                _logger.AddLogItemToQueue(ex.Message, ex, "Exception");
            }
            
        }

        private async Task GetXmlData(Category category)
        {
            using (var client = new HttpClient())

            {

                // Make a GET request to the URL 

                var response = await client.GetAsync(category.url);

                // Ensure the response was successful 

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException exc)
                {

                    _logger.AddLogItemToQueue($"Unseccefull GET request of: {category.name},{category.source}", exc, "Error");
                }

                // Read the content of the response 

                var content = await response.Content.ReadAsStringAsync();

                // Output the content of the response 

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                //--
                WebsiteCoordination(doc, category);
            }
        }
     

        private void WebsiteCoordination(XmlDocument doc,Category category)
        {
            switch (category.source) 
            {
                case "globes":
                    _website = new Globes(_dataLayer,_logger);
                    break;
                case "ynet":
                    _website = new Ynet(_dataLayer, _logger);
                    break;
                case "maariv":
                    _website = new Maariv(_dataLayer,_logger);
                    break;
                case "walla":
                   _website = new Walla(_dataLayer,_logger);
                    break;
                default:
                    _logger.AddLogItemToQueue("Category source was not found", null, "Error");
                    break;
            }

            _website.FeedExtraction(doc, category);
        }


    }
}
