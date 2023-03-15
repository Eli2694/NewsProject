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
        public object AllCategories()
        {
            try
            {
                return DataLayer.Data.CategoryRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}
