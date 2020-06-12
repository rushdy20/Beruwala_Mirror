using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Beruwala_Mirror.Models.Extensions;

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

        public int NumberOfVisits { get; set; }

        [Display(Name = "Set as Top News Item for Days")]
        public int TopNewsForDays { get; set; }

        public DateTime DisplayDate { get; set; }

        public string ShortBody
        {
            
            get { return this.NewsBody.GetSubString(100); }
        }
    }
}
