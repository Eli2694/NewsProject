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
                string answer = MainManager.Instance.UserEnt.AddUserToDatabase(email);
                if (answer == "Ok")
                {
                    return Ok();
                }
                else
                {
                    return NotFound(answer);
                }
            }
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex,"Exception");
                return BadRequest();
            }   

        }

        [HttpPut]
        public IActionResult UpdateUserCategories(Users updateUser)
        {
            if (updateUser == null) { return BadRequest(); }

            try
            {
                string answer = MainManager.Instance.UserEnt.UpdateUserCategoriesInDB(updateUser);
                if (answer == "Ok")
                {
                    return Ok();
                }
                else
                {
                    return NotFound(answer);
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
                object answer = MainManager.Instance.UserEnt.GetUserCategoriesFromDb(email);
                if(answer is string)
                {
                   return NotFound((string)answer);
                }
                else
                {
                    return Ok(answer);
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
