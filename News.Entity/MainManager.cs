using Logger;
using News.DAL;
using News.Entity.LogicForApi;
using News.Entity.Websites;

namespace News.Entity
{
    public class MainManager
    {
        public LogManager? Log { get; set; }
        public ManageRssFeeds? Feeds { get; set; }
        public Globes Globes { get; set; }
        public Maariv Maariv { get; set; }
        public Ynet Ynet { get; set; }
        public Walla Walla { get; set; }
        public ArticleEntity ArticleEnt { get; set; }
        public CategoryEntity CategoryEnt { get; set; }
        public UserEntity UserEnt { get; set; }

        // Constructor with dependency injection
        public MainManager(DataLayer dataLayer, LogManager log)
        {
            Log = log;
            Feeds = new ManageRssFeeds(dataLayer,Log);
            Globes = new Globes(dataLayer, Log);
            Maariv = new Maariv(dataLayer, Log);
            Ynet = new Ynet(dataLayer,Log);
            Walla = new Walla(dataLayer, Log);
            ArticleEnt = new ArticleEntity(dataLayer);
            CategoryEnt = new CategoryEntity(dataLayer);
            UserEnt = new UserEntity(dataLayer);
        }
    }
}

