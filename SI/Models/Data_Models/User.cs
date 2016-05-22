using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class User
    {
        public string Id { get; set; }

        [Column("UserName")]
        public string Name { get; set; }

        [Column("Avatar")]
        public string AvatarName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Subcomment> Subcomments { get; set; }



        public virtual ICollection<PostVote> PostVotes { get; set; }

        public virtual ICollection<CommentVote> CommentVotes { get; set; }

        public virtual ICollection<SubcommentVote> SubcommentVotes { get; set; }

        public bool AllowNSFW { get; set; }
    }
    //public class EditUserViewModel
    //{
    //    public string avatar { get; set; }
    //}
}