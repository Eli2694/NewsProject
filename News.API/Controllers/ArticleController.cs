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
        private readonly MainManager _mainManager;

        public ArticleController(MainManager mainManager)
        {
            _mainManager = mainManager;
        }

        [HttpGet]
        public IActionResult GetMainUserArticles(string email)
        {

            if (email == null) { return BadRequest(); }

            try
            {
                var articles = _mainManager.ArticleEnt.MainUserArticles(email);
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
                _mainManager.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateArticleClicks(Article article)
        {

            if (article == null) { return BadRequest(); }

            try
            {
                string answer = _mainManager.ArticleEnt.UpdateNumberOfArticleClicks(article);
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

                _mainManager.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }      
            
        }

        [HttpPost]
        public IActionResult AddOrUpdateUserClicks(Article article,string email)
        {

            if (article == null || email == null ) { return BadRequest(); }

            try
            {
                string answer = _mainManager.ArticleEnt.UpdateTableUserClicksDB(article,email);

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

        [HttpGet("Popular")]

        public IActionResult GetPopularArticles(string email)
        {

            if (email == null) { return BadRequest(); }

            try
            {
                var articles = _mainManager.ArticleEnt.PopularArticles(email);
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

                _mainManager.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }
    
        }

        [HttpGet("Curious")]

        public IActionResult GetCuriousArticle(string email)
        {
            if (email == null) { return BadRequest(); }
            try
            {
                var articles = _mainManager.ArticleEnt.CuriousArticles(email);
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

                _mainManager.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                return BadRequest();
            }

        }

    }
}
