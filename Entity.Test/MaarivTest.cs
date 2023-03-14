using Logger;
using News.DAL;
using News.Entity.Websites;
using News.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FluentAssertions;

namespace Entity.Test
{
    public class MaarivTest
    {

        [Test, SetUp]
        [Category("Database")]
        public void CleanDB()
        {
            var articleList = DataLayer.Data.ArticleRepository.GetAll();
            DataLayer.Data.Article.RemoveRange(articleList);
        }

        [Test, Order(0)]
        [Category("HttpRequest")]
        public async Task GetWallaXmlData()
        {
            using (var client = new HttpClient())
            {

                // Make a GET request to the URL 

                var response = await client.GetAsync("https://www.maariv.co.il/Rss/RssFeedsAsakim");

                // Ensure the response was successful 

                response.EnsureSuccessStatusCode();


                // Read the content of the response 

                var content = await response.Content.ReadAsStringAsync();

                // Output the content of the response 

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);


                LogManager log = new LogManager();


                Maariv maariv = new Maariv(log);
                maariv.FeedExtraction(doc, new Category() { id = 17, name = "Business", url = "https://www.maariv.co.il/Rss/RssFeedsAsakim", source = "maariv" });

            }
        }

        [Test, Order(1)]
        [Category("Database")]

        public void CheckArticlesDB()
        {
            var articleList = DataLayer.Data.ArticleRepository.GetAll();
            articleList.Should().HaveCountGreaterThan(0);
        }
    }
}
