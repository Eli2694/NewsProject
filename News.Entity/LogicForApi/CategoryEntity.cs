using Logger;
using News.DAL;
using News.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Entity.LogicForApi
{
    public class CategoryEntity 
    {
        private readonly DataLayer _dataLayer;

        public CategoryEntity(DataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        public object AllCategories()
        {
            try
            {
                return _dataLayer.CategoryRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}
