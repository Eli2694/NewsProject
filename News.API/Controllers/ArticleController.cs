using Microsoft.AspNetCore.Mvc;
using News.DAL;
using News.Entity;
using News.Model;

namespace News.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMainUserArticles(string email)
        {
            try
            {
                Users user = DataLayer.Data.Users.FirstOrDefault(u=>u.Email == email);
                if (user == null)
                {
                    return NoContent();
                }
                else
                {

                    var articles = DataLayer.Data.Article
                                                    .Where(a =>
                                                        (user.FirstCategoryID != 0 && a.CategoryID == user.FirstCategoryID) ||
                                                        (user.SecondCategoryID != 0 && a.CategoryID == user.SecondCategoryID) ||
                                                        (user.ThirdCategoryID != 0 && a.CategoryID == user.ThirdCategoryID)
                                                    )
                                                    .OrderByDescending(a => a.CreatedDate)
                                                    .GroupBy(a => a.CategoryID)
                                                    .SelectMany(g => g.Take(10))
                                                    .ToList();

                    if(articles.Count() < 1)
                    {
                        return NoContent();
                    }

                    return Ok(articles);
                }
                
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
