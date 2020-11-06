using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Beruwala_Mirror.Models.Admin
{
    public class AdvertiseModel
    {
        public string Id { get; set; }

        [Display(Name = "Name ")] public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public int NoOfDaysToAdvertise { get; set; }

        [Display(Name = "Main Image")] public IFormFile MainImage { get; set; }

        public string FileName { get; set; }
    }
}