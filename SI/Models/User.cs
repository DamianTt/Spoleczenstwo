using System;
using System.Collections.Generic;
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

        public ICollection<Post> Posts { get; set; }

    }
}