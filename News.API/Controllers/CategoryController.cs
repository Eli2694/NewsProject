using Microsoft.AspNetCore.Mvc;
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
            var categories = MainManager.Instance.DataLayer.CategoryRepository.GetAll();
            return Ok(categories);
        } 
    }
}
