using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using News.Entity.Websites;
using static Logger.LogManager;

namespace News.Entity
{
    public class MainManager
    {
        public LogManager? Log { get; set; }

        public ManageRssFeeds? Feeds { get; set; }

        public Globes Globes { get; set; }

        //constructor
        private MainManager()
        {
            init();
        }

        // Singleton variable
        private static readonly MainManager _instance = new MainManager();
        public static MainManager Instance
        {
            get { return _instance; }
        }

        private void init()
        {
            Target(LogProvider.File);
            Log = new LogManager();

            Feeds = new ManageRssFeeds(Log);

            Globes = new Globes(Log);


        }
    }
}
