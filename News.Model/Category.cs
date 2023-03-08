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
        public int Id { get; set; }

        [StringLength(20)] // nvarchar(50)
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public string URL { get; set; } 

        public string Source { get; set; } = string.Empty;

    }
}
