using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image Path")]
        public string ImgName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}