using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beruwala_Mirror.Models.Admin
{
  public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public int Order { get; set; }
    }
}
