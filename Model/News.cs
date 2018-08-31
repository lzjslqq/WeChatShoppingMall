using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class News : ModelBase
    {
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public int ClassId { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
    }
}