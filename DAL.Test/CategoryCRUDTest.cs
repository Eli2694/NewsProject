using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News.DAL;
using News.Model;

namespace DAL.Test
{
    [TestFixture]
    public class CategoryCRUDTest
    {
        [Test]
        [Category("CRUD")]
        [Order(0)]
        public void GetAllCategories()
        {
            List<Category> AllCategories = (List<Category>)DataLayer.Data.CategoryRepository.GetAll();
            Category category = AllCategories.First();
            Assert.IsNotNull(category);
            Assert.AreEqual("globes", category.name );
            Assert.AreEqual("https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=9010", category.url);
        }


        [Test]
        [Category("CRUD")]
        [Order(1)]
        public void AddCategory()
        {           
           DataLayer.Data.CategoryRepository.Insert(new Category() { name = "News",url = "https://www.Test.co.il",source ="Test" });
        }

        [Test]
        [Category("CRUD")]
        [Order(2)]
        public void GetCategory()
        {
            Category category =  DataLayer.Data.CategoryRepository.GetById(35); 
            Assert.IsNotNull(category);
            Assert.AreEqual("News", category.name);
        }

        [Test]
        [Category("CRUD")]
        [Order(3)]
        public void UpdateCategory()
        {
            Category category = DataLayer.Data.CategoryRepository.GetById(35);
            category.name = "Test";
            DataLayer.Data.CategoryRepository.Update(category);

            Category updatedCategory = DataLayer.Data.CategoryRepository.GetById(35);

            Assert.IsNotNull(updatedCategory);
            Assert.AreEqual("Test", updatedCategory.name);
        }

        [Test]
        [Category("CRUD")]
        [Order(4)]
        public void DeleteCategory()
        {
            DataLayer.Data.CategoryRepository.Delete(35);
        }
    }
}
