using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}