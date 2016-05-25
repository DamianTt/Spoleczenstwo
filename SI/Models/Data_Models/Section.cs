using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class Section
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }



        public virtual ICollection<Post> Posts { get; set; }

    }
}