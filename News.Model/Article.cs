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
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public string ArticleLink { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public string Source { get; set; } = string.Empty;

        public int CategoryID { get; set; }

        public string Guid { get; set; }

        public int ArticleClicks { get; set; }
    }
}
