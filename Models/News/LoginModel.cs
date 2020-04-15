using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Beruwala_Mirror.Models.News
{
    public class LoginModel
    {
        [Display(Name="Email address")]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
