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
                Users user = DataLayer.Data.Users.ToList().Find(x => x.Email == email);
                if (user == null)
                {
                    DataLayer.Data.UsersRepository.Insert(new Users() { Email = email });
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
                Users user = DataLayer.Data.Users.ToList().Find(x => x.Email == updateUser.Email);
                if (user != null)
                {
                    user.FirstCategoryID = updateUser.FirstCategoryID;
                    user.SecondCategoryID = updateUser.SecondCategoryID;
                    user.ThirdCategoryID = updateUser.ThirdCategoryID;

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
                Users user = DataLayer.Data.Users.ToList().Find(x => x.Email == email);
                if (user != null)
                {
                    if (user.FirstCategoryID == 0 && user.SecondCategoryID == 0 && user.ThirdCategoryID == 0)
                    {
                        return NotFound("User does not have any categories selected.");
                    }
                    else
                    {
                        List<Category> userCategories = DataLayer.Data.Categories
                       .Where(c => c.Id == user.FirstCategoryID || c.Id == user.SecondCategoryID || c.Id == user.ThirdCategoryID)
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
