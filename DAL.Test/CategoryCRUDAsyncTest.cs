using News.DAL;
using News.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Test
{
    [TestFixture]
    public class CategoryCRUDAsyncTest
    {
        [Test]
        [Category("CRUDAsync")]
        public async Task  GetAllCategories()
        {
            //Task<IEnumerable<Category>> allCategoriesTask = DataLayer.Data.CategoryRepository.GetAllAsync();
            //IEnumerable<Category> allCategories = await allCategoriesTask;
            //List<Category> categoryList = allCategories.ToList();
            //Category category = categoryList.First();


            /* 
             In this code, GetAllAsync() returns a Task<IEnumerable<Category>> that is awaited to retrieve the IEnumerable<Category> of all categories. This collection is then used to create a new List<Category> using the constructor that takes an IEnumerable<Category> as input. Finally, the first category in the list is retrieved using the First() method.
             */

            IEnumerable<Category> allCategories = await DataLayer.Data.CategoryRepository.GetAllAsync();
            List<Category> categoryList = new List<Category>(allCategories);
            Category category = categoryList.First();

            Assert.IsNotNull(category);
            Assert.AreEqual("globes", category.source);
            Assert.AreEqual("https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=9010", category.url);
        }
    }
}
