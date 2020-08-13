using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beruwala_Mirror.Models.News;
using Beruwala_Mirror.Models.Users;

namespace Beruwala_Mirror.Models.Admin
{
    public class AdminModel
    {
        public NewsCategories NewsCategories { get; set; }
        public CategoryModel AddCategory { get; set; }

        public IEnumerable<NewsModel> NewsLists { get; set; }

        public IEnumerable<UserModel> Users { get; set; }
    }
}
