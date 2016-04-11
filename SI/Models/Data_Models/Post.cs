using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(140)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image Path")]
        public string ImgName { get; set; }

        public DateTime Date { get; set; }

        public bool NSFW { get; set; }



        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        public string AuthorId { get; set; }



        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<PostVote> Votes { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}