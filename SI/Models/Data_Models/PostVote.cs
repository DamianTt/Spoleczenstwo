using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class PostVote
    {
        public DateTime Date { get; set; }

        public bool IsUpvote { get; set; }
        


        [ForeignKey("UserId")]
        public User User { get; set; }
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        [Key, Column(Order = 1)]
        public string PostId { get; set; }
    }
}