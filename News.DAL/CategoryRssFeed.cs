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
                new Category {name = "Tourism",url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=9010",source="globes"},
                new Category {name = "Car",url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=3220",source = "globes"},
                new Category {name = "Technology",url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=594",source = "globes"},
                new Category {name = "Israel",url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=9917",source = "globes"},
                new Category {name = "Global",url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=1225",source = "globes"},
                new Category {name = "Real estate",url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=607",source = "globes"},
                new Category {name = "Capital Market",url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=829",source = "globes"},
                new Category {name = "Career",url="https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iid=3266",source = "globes"},
                //Ynet
                new Category {name ="Sport",url="https://www.ynet.co.il/Integration/StoryRss3.xml",source="ynet"},
                new Category {name ="Health",url="https://www.ynet.co.il/Integration/StoryRss1208.xml",source="ynet"},
                new Category {name ="Food",url="https://www.ynet.co.il/Integration/StoryRss975.xml",source="ynet"},
                new Category {name ="Economy",url="https://www.ynet.co.il/Integration/StoryRss6.xml",source="ynet"},
                new Category {name ="Science and nature",url="https://www.ynet.co.il/Integration/StoryRss2142.xml",source="ynet"},
                new Category {name ="Car",url="https://www.ynet.co.il/Integration/StoryRss550.xml",source="ynet"},
                new Category {name ="Tourism",url="https://www.ynet.co.il/Integration/StoryRss598.xml",source="ynet"},
                new Category {name ="Computers",url="https://www.ynet.co.il/Integration/StoryRss544.xml",source="ynet"},
                //Maariv
                new Category {name ="Business",url="https://www.maariv.co.il/Rss/RssFeedsAsakim",source="maariv"},
                new Category {name ="Sport",url="https://www.maariv.co.il/Rss/RssFeedsSport",source="maariv"},
                new Category {name ="Technology",url="https://www.maariv.co.il/Rss/RssFeedsTechnologeya",source="maariv"},
                new Category {name ="Military",url="https://www.maariv.co.il/Rss/RssFeedsZavaVeBetachon",source="maariv"},
                new Category {name ="Music",url="https://www.maariv.co.il/Rss/RssFeedsMozika",source="maariv"},
                new Category {name ="Fashion",url="https://www.maariv.co.il/Rss/RssFeedsOfna",source="maariv"},
                new Category {name ="Law",url="https://www.maariv.co.il/Rss/RssMishpat",source="maariv"},
                new Category {name ="Parents",url="https://www.maariv.co.il/Rss/RssFeedsNewParents",source="maariv"},
                //Walla
                new Category {name="Basketball",url="https://rss.walla.co.il/feed/151",source="walla"},
                new Category {name="Military",url="https://rss.walla.co.il/feed/2689",source="walla"},
                new Category {name="Israel",url="https://rss.walla.co.il/feed/1",source="walla"},
                new Category {name="Global",url="https://rss.walla.co.il/feed/2",source="walla"},
                new Category {name="Fashion Trends",url="https://rss.walla.co.il/feed/2101",source="walla"},
                new Category {name="Technology",url="https://rss.walla.co.il/feed/4000",source="walla"},
                new Category {name="Games",url="https://rss.walla.co.il/feed/2266",source="walla"},
                new Category {name="Tourism",url="https://rss.walla.co.il/feed/2500",source="walla"}
            };
        }
    }
}
