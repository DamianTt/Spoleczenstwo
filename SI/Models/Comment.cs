using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1024)]
        public string Body { get; set; }

        public int PostId { get; set; }



        public Comment() { }

        public Comment(int PostId)
        {
            this.PostId = PostId;
        }

    }
}