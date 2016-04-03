using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class SIDb : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public SIDb() : base("name=DefaultConnection")
        {

        }

    }
}