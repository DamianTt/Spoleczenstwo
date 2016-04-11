using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class Section
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}