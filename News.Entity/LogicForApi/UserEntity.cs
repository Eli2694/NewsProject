using Logger;
using News.DAL;
using News.Entity.Base;
using News.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Entity.LogicForApi
{
    public class UserEntity 
    {
        public string AddUserToDatabase(string email)
        {
            try
            {
                Users user = DataLayer.Data.Users.ToList().Find(x => x.email == email);

                if (user == null)
                {
                    DataLayer.Data.UsersRepository.Insert(new Users() { email = email });
                    return "Ok";
                }

                return "User Not Found In DB";
            }
            catch (Exception)
            {

                throw;
            }    
        }

        public string UpdateUserCategoriesInDB(Users updateUser)
        {
            try
            {
                Users user = DataLayer.Data.Users.ToList().Find(x => x.email == updateUser.email);
                if (user != null)
                {
                    user.firstCategoryID = updateUser.firstCategoryID;
                    user.secondCategoryID = updateUser.secondCategoryID;
                    user.thirdCategoryID = updateUser.thirdCategoryID;

                    DataLayer.Data.UsersRepository.Update(user);
                    return "Ok";
                }
                else
                {
                    return "User Not Found In DB";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public object GetUserCategoriesFromDb(string email) 
        {
            try
            {
                Users user = DataLayer.Data.Users.ToList().Find(x => x.email == email);

                if (user != null)
                {
                    if (user.firstCategoryID == 0 && user.secondCategoryID == 0 && user.thirdCategoryID == 0)
                    {
                        return "User Did Not Choose Categories";
                    }
                    else
                    {
                        // First get all categories
                        var allCategories = DataLayer.Data.CategoryRepository.GetAll();

                        // Then select specific categories
                        var userCategories = allCategories
                            .Where(c => c.id == user.firstCategoryID || c.id == user.secondCategoryID || c.id == user.thirdCategoryID)
                            .ToList();

                        // if the count of extracted categories is exactly 3
                        if (userCategories.Count() == 3)
                        {
                            return userCategories;
                        }
                        else
                        {
                            return "Can't Find Categories";
                        }
                    }

                }
                else
                {
                    return "User Not Found In DB";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
