namespace SI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);








            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 1000),
                        Date = c.DateTime(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.CommentVotes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CommentId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsUpvote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CommentId })
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 140),
                        ImgName = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        NSFW = c.Boolean(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostVotes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsUpvote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.PostId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Subcomments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 1000),
                        Date = c.DateTime(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.SubcommentVotes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        SubcommentId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsUpvote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SubcommentId })
                .ForeignKey("dbo.Subcomments", t => t.SubcommentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SubcommentId);
            
            CreateTable(
                "dbo.SectionPosts",
                c => new
                    {
                        Section_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Section_Id, t.Post_Id })
                .ForeignKey("dbo.Sections", t => t.Section_Id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .Index(t => t.Section_Id)
                .Index(t => t.Post_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubcommentVotes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubcommentVotes", "SubcommentId", "dbo.Subcomments");
            DropForeignKey("dbo.Subcomments", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Subcomments", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostVotes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostVotes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.SectionPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.SectionPosts", "Section_Id", "dbo.Sections");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentVotes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentVotes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.SectionPosts", new[] { "Post_Id" });
            DropIndex("dbo.SectionPosts", new[] { "Section_Id" });
            DropIndex("dbo.SubcommentVotes", new[] { "SubcommentId" });
            DropIndex("dbo.SubcommentVotes", new[] { "UserId" });
            DropIndex("dbo.Subcomments", new[] { "CommentId" });
            DropIndex("dbo.Subcomments", new[] { "AuthorId" });
            DropIndex("dbo.PostVotes", new[] { "PostId" });
            DropIndex("dbo.PostVotes", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropIndex("dbo.CommentVotes", new[] { "CommentId" });
            DropIndex("dbo.CommentVotes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropTable("dbo.SectionPosts");
            DropTable("dbo.SubcommentVotes");
            DropTable("dbo.Subcomments");
            DropTable("dbo.PostVotes");
            DropTable("dbo.Sections");
            DropTable("dbo.Posts");
            DropTable("dbo.CommentVotes");
            DropTable("dbo.Comments");


            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
