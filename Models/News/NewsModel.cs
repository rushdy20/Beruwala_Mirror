using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beruwala_Mirror.Models.News
{
    public class NewsModel
    {
        public string Id { get; set; }
        public string Heading { get; set; }
        public string MainImg { get; set; }
        public IEnumerable<string> Images { get; set; }
        public string NewsBody { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
