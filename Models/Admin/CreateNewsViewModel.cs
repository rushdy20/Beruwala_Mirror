﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beruwala_Mirror.Models.Admin
{
    public class CreateNewsViewModel
    {
        public string Id { get; set; }

        [Display(Name = "News Heading")] public string Heading { get; set; }
        public string MainImg { get; set; }

        [Display(Name = "Main Image")] public IFormFile MainImage { get; set; }

        [Display(Name = "Supporting Image-1")] public IFormFile SubImage1 { get; set; }

        [Display(Name = "Supporting Image-2")] public IFormFile SubImage2 { get; set; }

        [Display(Name = "Supporting Image-3")] public IFormFile SubImage3 { get; set; }

        [Display(Name = "News Body")] public string NewsBody { get; set; }

        [Display(Name = "Created Date")] public string CreatedDate { get; set; }

        [Display(Name = "Created By")] public string CreatedBy { get; set; }

        [Display(Name = "YouTub Link")] public string YouTubLink { get; set; }
        [Display(Name = "News Category")]
        public string CategoryId { get; set; }
        public List<SelectListItem> NewsCategories { get; set; }

        [Display(Name = "Set as Top News Item for Days")]
        public int TopNewsForDays { get; set; }

    }
}