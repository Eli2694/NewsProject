using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Model
{
    public class Article
    {
        [Key]
        public int id { get; set; }

        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public string image { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public string link { get; set; } = string.Empty;

        public string createdDate { get; set; }  = string.Empty;

        public int categoryID { get; set; }

        public string guid { get; set; }

        public int articleClicks { get; set; }
    }
}
