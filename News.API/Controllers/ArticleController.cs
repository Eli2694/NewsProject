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
                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateArticleClicks(Article article)
        {
            try
            {
                Article art = DataLayer.Data.ArticleRepository.GetById(article.id);
                if (art != null)
                {
                    art.articleClicks = art.articleClicks + 1;
                    DataLayer.Data.ArticleRepository.Update(art);
                    return Ok(art);
                }

                return NoContent();
            }
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }      
            
        }

        [HttpPost]
        public IActionResult AddOrUpdateUserClicks(Article article,string email)
        {
            try
            {
                Users user = DataLayer.Data.Users.FirstOrDefault(u => u.email == email);
                if (user == null)
                {
                    return NoContent();
                }
                else
                {
                    UserClick userClick = DataLayer.Data.UserClickRepository.GetById(user.id);
                    if (userClick == null)
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
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }

           
        }

        [HttpGet("Popular")]

        public IActionResult GetPopularArticles(string email)
        {
            try
            {
                Users user = DataLayer.Data.Users.FirstOrDefault(u => u.email == email);
                if (user == null)
                {
                    return NoContent();
                }
                else
                {
                    var popularArticles = DataLayer.Data.Article
                        .Where(a => a.articleClicks > 0 && (a.categoryID == user.firstCategoryID || a.categoryID == user.secondCategoryID || a.categoryID == user.thirdCategoryID))
                        .OrderByDescending(a => a.articleClicks)
                        .Take(10)
                        .ToList();

                    return Ok(popularArticles);
                }
            }
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }
    
        }

        [HttpGet("Curious")]

        public IActionResult GetCuriousArticle(string email)
        {

            try
            {
                Users user = DataLayer.Data.Users.FirstOrDefault(u => u.email == email);
                if (user != null)
                {
                    var unclickedArticles = DataLayer.Data.Article
                                                            .GroupJoin(
                                                                DataLayer.Data.UserClicks.Where(u => u.userId == user.id),
                                                                a => a.id,
                                                                uc => uc.articleID,
                                                                (a, uc) => new { Article = a, UserClicks = uc })
                                                            .Where(x => !x.UserClicks.Any())
                                                            .OrderByDescending(a => a.Article.createdDate)
                                                            .Select(x => x.Article)
                                                            .Take(10)
                                                            .ToList();


                    return Ok(unclickedArticles);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }

           
           

        }

    }
}
