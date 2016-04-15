using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SI.Models
{
    public class SIDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Section> Sections { get; set; }



        public SIDb() : base("name=DefaultConnection") { }

        public bool TrySaveChanges()
        {
            try
            {
                SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("dbo.AspNetUsers");

            modelBuilder.Entity<Post>()
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }

    }
}