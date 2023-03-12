using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Model
{
    public class Category
    {
        [Key]
        public int id { get; set; }

        [StringLength(20)] // nvarchar(50)
        public string name { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public string url { get; set; } 

        public string source { get; set; } = string.Empty;

    }
}
