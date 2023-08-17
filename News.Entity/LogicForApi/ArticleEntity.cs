using News.DAL;
using News.Model;

namespace News.Entity.LogicForApi
{
    public class ArticleEntity 
    {

        private readonly DataLayer _dataLayer;

        public ArticleEntity(DataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        public object MainUserArticles(string email)
        {
            try
            {
                Users user = _dataLayer.Users.FirstOrDefault(u => u.email == email);
                if (user == null)
                {
                    return "User Not Found In DB";
                }
                else
                {

                    var articles = _dataLayer.Article
                                                    .Where(a =>
                                                        (user.firstCategoryID != 0 && a.categoryID == user.firstCategoryID) ||
                                                        (user.secondCategoryID != 0 && a.categoryID == user.secondCategoryID) ||
                                                        (user.thirdCategoryID != 0 && a.categoryID == user.thirdCategoryID)
                                                    )
                                                    .OrderByDescending(a => a.createdDate)
                                                    .GroupBy(a => a.categoryID)
                                                    .SelectMany(g => g.Take(10))
                                                    .ToList();

                    if (articles.Count() < 1)
                    {
                        return "Article Count Is Zero";
                    }

                    return articles;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateNumberOfArticleClicks(Article article) 
        {
            try
            {
                Article art = _dataLayer.ArticleRepository.GetById(article.id);
                if (art != null)
                {
                    art.articleClicks = art.articleClicks + 1;
                    _dataLayer.ArticleRepository.Update(art);
                    return "Ok";
                }

                return "Article Not Found In Db";
            }
            catch (Exception)
            {

                throw;
            }       
        }

        public string UpdateTableUserClicksDB(Article article, string email)
        {

            try
            {
                Users user = _dataLayer.Users.FirstOrDefault(u => u.email == email);

                if (user == null)
                {
                    return "User Not Found In DB";
                }
                else
                {
                    UserClick userClick = _dataLayer.UserClickRepository.GetById(user.id);
                    if (userClick == null)
                    {
                        userClick = new UserClick()
                        {
                            userId = user.id,
                            articleID = article.id,
                            numberOfClicks = 1
                        };

                        _dataLayer.UserClickRepository.Insert(userClick);
                        return "Ok";
                    }
                    else
                    {
                        userClick.numberOfClicks = userClick.numberOfClicks + 1;
                        _dataLayer.UserClickRepository.Update(userClick);
                        return "Ok";
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }    

        }

        public object PopularArticles(string email)
        {
            try
            {
                Users user = _dataLayer.Users.FirstOrDefault(u => u.email == email);
                if (user == null)
                {
                    return "User Not Found In DB";
                }
                else
                {
                    var popularArticles = _dataLayer.Article
                        .Where(a => a.articleClicks > 0 && (a.categoryID == user.firstCategoryID || a.categoryID == user.secondCategoryID || a.categoryID == user.thirdCategoryID))
                        .OrderByDescending(a => a.articleClicks)
                        .Take(10)
                        .ToList();

                    return popularArticles;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public object CuriousArticles(string email)
        {
            try
            {
                Users user = _dataLayer.Users.FirstOrDefault(u => u.email == email);
                if (user != null)
                {
                    var unclickedArticles = _dataLayer.Article
                                                            .GroupJoin(
                                                                _dataLayer.UserClicks.Where(u => u.userId == user.id),
                                                                a => a.id,
                                                                uc => uc.articleID,
                                                                (a, uc) => new { Article = a, UserClicks = uc })
                                                            .Where(x => !x.UserClicks.Any())
                                                            .OrderByDescending(a => a.Article.createdDate)
                                                            .Select(x => x.Article)
                                                            .Take(10)
                                                            .ToList();


                    return unclickedArticles;
                }
                return "User Not Found In DB";
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
