using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Model
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }
        public string auth0Bearer { get; set; } = string.Empty;

        


    }
}
