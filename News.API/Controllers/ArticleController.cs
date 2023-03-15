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

            if (email == null) { return BadRequest(); }

            try
            {
                var articles = MainManager.Instance.ArticleEnt.MainUserArticles(email);
                if(articles is string)
                {
                    return NotFound(articles);
                }
                else
                {
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

            if (article == null) { return BadRequest(); }

            try
            {
                string answer = MainManager.Instance.ArticleEnt.UpdateNumberOfArticleClicks(article);
                if(answer == "Ok")
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

        [HttpPost]
        public IActionResult AddOrUpdateUserClicks(Article article,string email)
        {

            if (article == null || email == null ) { return BadRequest(); }

            try
            {
                string answer = MainManager.Instance.ArticleEnt.UpdateTableUserClicksDB(article,email);

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

        [HttpGet("Popular")]

        public IActionResult GetPopularArticles(string email)
        {

            if (email == null) { return BadRequest(); }

            try
            {
                var articles = MainManager.Instance.ArticleEnt.PopularArticles(email);
                if (articles is string)
                {
                    return NotFound(articles);
                }
                else
                {
                    return Ok(articles);
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
            if (email == null) { return BadRequest(); }
            try
            {
                var articles = MainManager.Instance.ArticleEnt.CuriousArticles(email);
                if (articles is string)
                {
                    return NotFound(articles);
                }
                else
                {
                    return Ok(articles);
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
