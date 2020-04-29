using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Beruwala_Mirror.Models.News
{
    public class NewsModel
    {
        public string Id { get; set; }

        [Display(Name = "News Heading")]
        public string Heading { get; set; }
        public string MainImg { get; set; }
        public IEnumerable<string> Images { get; set; }

        [Display(Name = "News Body")]
        public string NewsBody { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "YouTub Link")]
        public string YouTubLink { get; set; }
    }
}
