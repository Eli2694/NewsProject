using Microsoft.AspNetCore.Mvc;
using News.DAL;
using News.Entity;
using News.Model;

namespace News.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("{email}")]
        public IActionResult AddUserToDB(string email)
        {
            if(email == null) { return BadRequest(); }

            try
            {
                Users user = DataLayer.Data.Users.ToList().Find(x => x.email == email);
                if (user == null)
                {
                    DataLayer.Data.UsersRepository.Insert(new Users() { email = email });
                    return Ok();
                }
            }
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex,"Exception");
                return BadRequest();
            }   

            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateUserCategories(Users updateUser)
        {
            if (updateUser == null) { return BadRequest(); }

            try
            {
                Users user = DataLayer.Data.Users.ToList().Find(x => x.email == updateUser.email);
                if (user != null)
                {
                    user.firstCategoryID = updateUser.firstCategoryID;
                    user.secondCategoryID = updateUser.secondCategoryID;
                    user.thirdCategoryID = updateUser.thirdCategoryID;

                    DataLayer.Data.UsersRepository.Update(user);
                    return Ok("Success");
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }

        }

        [HttpGet("{email}")]
        public IActionResult GetUserCategories(string email)
        {
            if (email == null) { return BadRequest(); }

            try
            {
                Users user = DataLayer.Data.Users.ToList().Find(x => x.email == email);
                if (user != null)
                {
                    if (user.firstCategoryID == 0 && user.secondCategoryID == 0 && user.thirdCategoryID == 0)
                    {
                        return NotFound("User does not have any categories selected.");
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
                            return Ok(userCategories);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                   
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }
        }
            
    }

}
