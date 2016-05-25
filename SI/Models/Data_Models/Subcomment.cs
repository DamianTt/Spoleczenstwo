using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class Subcomment
    {
        public long Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Body { get; set; }

        public DateTime Date { get; set; }



        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        public string AuthorId { get; set; }

        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
        public long CommentId { get; set; }


        
        public virtual ICollection<SubcommentVote> Votes { get; set; }

    }
}