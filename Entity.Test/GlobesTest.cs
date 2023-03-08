using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;
using News.Entity;
using News.Model;
using News.Entity.Websites;
using Logger;

namespace Entity.Test
{
    [TestFixture]
    public class GlobesTest
    {
        [Test,SetUp]
        [Category("HttpRequest")]
        public async Task GetGlobesXmlData()
        {
            using (var client = new HttpClient())
            {

                // Make a GET request to the URL 

                var response = await client.GetAsync("https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=9010");

                // Ensure the response was successful 
                
                  response.EnsureSuccessStatusCode();
                
           
                // Read the content of the response 

                var content = await response.Content.ReadAsStringAsync();

                // Output the content of the response 

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);


                LogManager log = new LogManager();

                //Globes Test
                Globes globe = new News.Entity.Websites.Globes(log);
                globe.FeedExtraction(doc, new Category() { Id = 1, Name = "Tourism", URL = "https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=9010", Source = "globes" });
                
            }
        }

    }
}
