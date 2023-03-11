using Microsoft.AspNetCore.Mvc;
using News.DAL;
using News.Entity;
using News.Model;

namespace News.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public CategoryController() { }

        [HttpGet]      
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = DataLayer.Data.CategoryRepository.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
               
                var errorMessage = "An error occurred";
                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = errorMessage });
            }
        }

        
    }
}
