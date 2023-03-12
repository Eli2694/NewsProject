using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Model
{
    public class Users
    {
        [Key]
        public int id { get; set; }

        [DataType(DataType.EmailAddress)]

        public string email { get; set; } = string.Empty;

        // Can be nullable
        public int firstCategoryID { get; set; }

        public int secondCategoryID { get; set; }

        public int thirdCategoryID { get; set; }


    }
}
