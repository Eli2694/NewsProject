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
                Users user = DataLayer.Data.Users.FirstOrDefault(u=>u.email == email);
                if (user == null)
                {
                    return NoContent();
                }
                else
                {

                    var articles = DataLayer.Data.Article
                                                    .Where(a =>
                                                        (user.firstCategoryID != 0 && a.categoryID == user.firstCategoryID) ||
                                                        (user.secondCategoryID != 0 && a.categoryID == user.secondCategoryID) ||
                                                        (user.thirdCategoryID != 0 && a.categoryID == user.thirdCategoryID)
                                                    )
                                                    .OrderByDescending(a => a.createdDate)
                                                    .GroupBy(a => a.categoryID)
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

        [HttpPut]
        public IActionResult UpdateArticleClicks(Article article)
        {
            Article art = DataLayer.Data.ArticleRepository.GetById(article.id);
            if (art != null) {
                art.articleClicks = art.articleClicks + 1;
                DataLayer.Data.ArticleRepository.Update(art);
                return Ok(art);
            }

            return NoContent();
            
        }

        [HttpPost]
        public IActionResult AddOrUpdateUserClicks(Article article,string email)
        {
            Users user = DataLayer.Data.Users.FirstOrDefault(u => u.email == email);
            if(user == null) 
            {
                return NoContent();
            }
            else
            {
                UserClick userClick = DataLayer.Data.UserClickRepository.GetById(user.id);
                if(userClick == null) 
                {
                    userClick = new UserClick()
                    {
                        userId = user.id,
                        articleID = article.id,
                        numberOfClicks = 1
                    };
                    DataLayer.Data.UserClickRepository.Insert(userClick);
                    return Ok(userClick);
                }
                else
                {
                    userClick.numberOfClicks = userClick.numberOfClicks + 1;
                    DataLayer.Data.UserClickRepository.Update(userClick);
                    return Ok(userClick);
                }
            }
        }
    }
}
