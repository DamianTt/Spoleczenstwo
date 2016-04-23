using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class SubcommentVote
    {
        public DateTime Date { get; set; }

        public bool IsUpvote { get; set; }


        
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        
        [ForeignKey("SubcommentId")]
        public virtual Subcomment Subcomment { get; set; }
        [Key, Column(Order = 1)]
        public long SubcommentId { get; set; }
    }
}