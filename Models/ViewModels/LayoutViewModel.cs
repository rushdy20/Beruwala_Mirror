using System.Collections.Generic;
using Beruwala_Mirror.Models.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beruwala_Mirror.Models.ViewModels
{
    public class LayoutViewModel
    {
        public List<SelectListItem> NewsCategory { get; set; }
        public List<AdvertiseModel> Advertisements { get; set; }
    }
}