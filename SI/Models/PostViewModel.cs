using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SI.Models
{
    public class NewPostViewModel
    {
        [Required]
        [StringLength(140)]
        public string Title { get; set; }

        [Required]
        public HttpPostedFileBase File { get; set; }

        public bool NSFW { get; set; }

        public string Tags { get; set; }
    }

    public class EditPostViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(140)]
        public string Title { get; set; }

        public string ImgName { get; set; }

        public bool NSFW { get; set; }

        public string Tags { get; set; }

    }
}