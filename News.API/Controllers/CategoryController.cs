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
        private readonly MainManager _mainManager;

        public CategoryController(MainManager mainManager)
        {
            _mainManager = mainManager;
        }

        [HttpGet]      
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _mainManager.CategoryEnt.AllCategories();
                if(categories == null) { return  NotFound("There are no categories in the database"); }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _mainManager.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }
        }

        
    }
}
