using Logger;
using News.Entity.Websites;
using News.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Entity.Test
{
    public class YnetTest
    {
        [Test, Order(1)]
        [Category("HttpRequest")]
        public async Task GetWallaXmlData()
        {
            using (var client = new HttpClient())
            {

                // Make a GET request to the URL 

                var response = await client.GetAsync("https://www.ynet.co.il/Integration/StoryRss3.xml");

                // Ensure the response was successful 

                response.EnsureSuccessStatusCode();


                // Read the content of the response 

                var content = await response.Content.ReadAsStringAsync();

                // Output the content of the response 

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);


                LogManager log = new LogManager();


                Ynet ynet = new Ynet(log);
                ynet.FeedExtraction(doc, new Category() { Id = 9, Name = "Sport", URL = "https://www.ynet.co.il/Integration/StoryRss3.xml", Source = "ynet" });

            }
        }
    }
}
