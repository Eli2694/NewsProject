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
        public int Id { get; set; }

        public int UserId { get; set; }

        /*The [ForeignKey] attribute specifies that the UserId column in the UserClicks table references the Id column in the User table. */
        [ForeignKey("UserId")]
        public Users User { get; set; }

        public int CategoryID { get; set; } 

        public int ArticleID { get; set; }

        public int NumberOfClicks { get; set; }

    }
}
