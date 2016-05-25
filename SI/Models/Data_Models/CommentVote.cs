using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class CommentVote
    {
        public DateTime Date { get; set; }

        public bool IsUpvote { get; set; }


        
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
        [Key, Column(Order = 1)]
        public long CommentId { get; set; }

    }
}