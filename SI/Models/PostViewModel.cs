using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

            

        public virtual ICollection<Section> Sections { get; set; }
    }
}