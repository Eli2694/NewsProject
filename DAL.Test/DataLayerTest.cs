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
            //init database
            DataLayer.Data.SaveChanges();
        }
     
    }
}