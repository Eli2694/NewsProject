using NUnit.Framework;
using News.DAL;

namespace DAL.Test
{
    [TestFixture]
    public class DataLayerTest
    {
        [Test]
        [SetUp]
        [Category("DataLayer")]

        public void SetUp() 
        {
            // This will drop and recreate the database based on the current model.
            // The true parameter indicates that the database should be re-created even if it already exists.
            DataLayer.Data.SaveChanges();
            //DataLayer.Data.Database.Initialize(true);
        }
     
    }
}