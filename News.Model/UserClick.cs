using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Model
{
    public class UserClick
    {
        [Key]
        public int id { get; set; }

        public int userId { get; set; }

        public int articleID { get; set; }

        public int numberOfClicks { get; set; }

    }
}
