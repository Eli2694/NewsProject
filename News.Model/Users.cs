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
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]

        public string Email { get; set; } = string.Empty;

        // Can be nullable
        public int FirstCategoryID { get; set; }

        public int SecondCategoryID { get; set; }

        public int ThirdCategoryID { get; set; }


    }
}
