using News.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL
{
    public partial class DataLayer
    {
        private List<Category> GetRSSFeeds()
        {
            return new List<Category>()
            {
                //Globes
                new Category {Name = "Tourism",URL="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=9010",Source="globes"},
                new Category {Name = "Car",URL="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=3220",Source = "globes"},
                new Category {Name = "Technology",URL="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=594",Source = "globes"},
                new Category {Name = "Israel",URL="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=9917",Source = "globes"},
                new Category {Name = "Global",URL="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=1225",Source = "globes"},
                new Category {Name = "Real estate",URL="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=607",Source = "globes"},
                new Category {Name = "Capital Market",URL="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=829",Source = "globes"},
                new Category {Name = "Career",URL="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=3266",Source = "globes"},
                //Ynet
                new Category {Name ="Sport",URL="https://www.ynet.co.il/Integration/StoryRss3.xml",Source="ynet"},
                new Category {Name ="Health",URL="https://www.ynet.co.il/Integration/StoryRss1208.xml",Source="ynet"},
                new Category {Name ="Food",URL="https://www.ynet.co.il/Integration/StoryRss975.xml",Source="ynet"},
                new Category {Name ="Economy",URL="https://www.ynet.co.il/Integration/StoryRss6.xml",Source="ynet"},
                new Category {Name ="Science and nature",URL="https://www.ynet.co.il/Integration/StoryRss2142.xml",Source="ynet"},
                new Category {Name ="Car",URL="https://www.ynet.co.il/Integration/StoryRss550.xml",Source="ynet"},
                new Category {Name ="Tourism",URL="https://www.ynet.co.il/Integration/StoryRss598.xml",Source="ynet"},
                new Category {Name ="Computers",URL="https://www.ynet.co.il/Integration/StoryRss544.xml",Source="ynet"},
                //Maariv
                new Category {Name ="Business",URL="https://www.maariv.co.il/Rss/RssFeedsAsakim",Source="maariv"},
                new Category {Name ="Sport",URL="https://www.maariv.co.il/Rss/RssFeedsSport",Source="maariv"},
                new Category {Name ="Technology",URL="https://www.maariv.co.il/Rss/RssFeedsTechnologeya",Source="maariv"},
                new Category {Name ="Military",URL="https://www.maariv.co.il/Rss/RssFeedsZavaVeBetachon",Source="maariv"},
                new Category {Name ="Music",URL="https://www.maariv.co.il/Rss/RssFeedsMozika",Source="maariv"},
                new Category {Name ="Fashion",URL="https://www.maariv.co.il/Rss/RssFeedsOfna",Source="maariv"},
                new Category {Name ="Law",URL="https://www.maariv.co.il/Rss/RssMishpat",Source="maariv"},
                new Category {Name ="Parents",URL="https://www.maariv.co.il/Rss/RssFeedsNewParents",Source="maariv"},
                //Walla
                new Category {Name="Basketball",URL="https://rss.walla.co.il/feed/151",Source="walla"},
                new Category {Name="Military",URL="https://rss.walla.co.il/feed/2689",Source="walla"},
                new Category {Name="Israel",URL="https://rss.walla.co.il/feed/1",Source="walla"},
                new Category {Name="Global",URL="https://rss.walla.co.il/feed/2",Source="walla"},
                new Category {Name="Fashion Trends",URL="https://rss.walla.co.il/feed/2101",Source="walla"},
                new Category {Name="Technology",URL="https://rss.walla.co.il/feed/4000",Source="walla"},
                new Category {Name="Games",URL="https://rss.walla.co.il/feed/2266",Source="walla"},
                new Category {Name="Tourism",URL="https://rss.walla.co.il/feed/2500",Source="walla"}
            };
        }
    }
}
