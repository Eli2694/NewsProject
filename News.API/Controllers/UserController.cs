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
        private readonly MainManager _mainManager;

        public UserController(MainManager mainManager)
        {
            _mainManager = mainManager;
        }

        [HttpPost("{email}")]
        public IActionResult AddUserToDB(string email)
        {
            if(email == null) { return BadRequest(); }

            try
            {
                string answer = _mainManager.UserEnt.AddUserToDatabase(email);
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

                _mainManager.Log.AddLogItemToQueue(ex.Message, ex,"Exception");
                return BadRequest();
            }   

        }

        [HttpPut]
        public IActionResult UpdateUserCategories(Users updateUser)
        {
            if (updateUser == null) { return BadRequest(); }

            try
            {
                string answer = _mainManager.UserEnt.UpdateUserCategoriesInDB(updateUser);
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
                _mainManager.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }

        }

        [HttpGet("{email}")]
        public IActionResult GetUserCategories(string email)
        {
            if (email == null) { return BadRequest(); }

            try
            {
                object answer = _mainManager.UserEnt.GetUserCategoriesFromDb(email);
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

                _mainManager.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }
        }
            
    }

}
