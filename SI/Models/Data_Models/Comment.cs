using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Body { get; set; }

        public DateTime Date { get; set; }



        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        public string AuthorId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        public int PostId { get; set; }



        public virtual ICollection<Subcomment> Subcomments { get; set; }

        public virtual ICollection<CommentVote> Votes { get; set; }



        public Comment() { }
        
        public Comment(int PostId)
        {
            this.PostId = PostId;
        }

    }
}