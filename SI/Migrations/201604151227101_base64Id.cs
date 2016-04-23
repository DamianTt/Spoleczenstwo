namespace SI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class base64Id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.CommentVotes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Subcomments", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.SectionPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.PostVotes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.SubcommentVotes", "SubcommentId", "dbo.Subcomments");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.CommentVotes", new[] { "CommentId" });
            DropIndex("dbo.PostVotes", new[] { "PostId" });
            DropIndex("dbo.Subcomments", new[] { "CommentId" });
            DropIndex("dbo.SubcommentVotes", new[] { "SubcommentId" });
            DropIndex("dbo.SectionPosts", new[] { "Post_Id" });
            DropPrimaryKey("dbo.Comments");
            DropPrimaryKey("dbo.CommentVotes");
            DropPrimaryKey("dbo.Posts");
            DropPrimaryKey("dbo.PostVotes");
            DropPrimaryKey("dbo.Subcomments");
            DropPrimaryKey("dbo.SubcommentVotes");
            DropPrimaryKey("dbo.SectionPosts");
            AlterColumn("dbo.Comments", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Comments", "PostId", c => c.String(maxLength: 7));
            AlterColumn("dbo.CommentVotes", "CommentId", c => c.Long(nullable: false));
            
            DropColumn("dbo.Posts", "Id");
            AddColumn("dbo.Posts", "Id", c => c.String(nullable: false, maxLength: 7));

            AlterColumn("dbo.PostVotes", "PostId", c => c.String(nullable: false, maxLength: 7));
            AlterColumn("dbo.Subcomments", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Subcomments", "CommentId", c => c.Long(nullable: false));
            AlterColumn("dbo.SubcommentVotes", "SubcommentId", c => c.Long(nullable: false));
            AlterColumn("dbo.SectionPosts", "Post_Id", c => c.String(nullable: false, maxLength: 7));
            AddPrimaryKey("dbo.Comments", "Id");
            AddPrimaryKey("dbo.CommentVotes", new[] { "UserId", "CommentId" });
            AddPrimaryKey("dbo.Posts", "Id");
            AddPrimaryKey("dbo.PostVotes", new[] { "UserId", "PostId" });
            AddPrimaryKey("dbo.Subcomments", "Id");
            AddPrimaryKey("dbo.SubcommentVotes", new[] { "UserId", "SubcommentId" });
            AddPrimaryKey("dbo.SectionPosts", new[] { "Section_Id", "Post_Id" });
            CreateIndex("dbo.Comments", "PostId");
            CreateIndex("dbo.CommentVotes", "CommentId");
            CreateIndex("dbo.PostVotes", "PostId");
            CreateIndex("dbo.Subcomments", "CommentId");
            CreateIndex("dbo.SubcommentVotes", "SubcommentId");
            CreateIndex("dbo.SectionPosts", "Post_Id");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CommentVotes", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subcomments", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SectionPosts", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostVotes", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubcommentVotes", "SubcommentId", "dbo.Subcomments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubcommentVotes", "SubcommentId", "dbo.Subcomments");
            DropForeignKey("dbo.PostVotes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.SectionPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Subcomments", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.CommentVotes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropIndex("dbo.SectionPosts", new[] { "Post_Id" });
            DropIndex("dbo.SubcommentVotes", new[] { "SubcommentId" });
            DropIndex("dbo.Subcomments", new[] { "CommentId" });
            DropIndex("dbo.PostVotes", new[] { "PostId" });
            DropIndex("dbo.CommentVotes", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropPrimaryKey("dbo.SectionPosts");
            DropPrimaryKey("dbo.SubcommentVotes");
            DropPrimaryKey("dbo.Subcomments");
            DropPrimaryKey("dbo.PostVotes");
            DropPrimaryKey("dbo.Posts");
            DropPrimaryKey("dbo.CommentVotes");
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.SectionPosts", "Post_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.SubcommentVotes", "SubcommentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Subcomments", "CommentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Subcomments", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PostVotes", "PostId", c => c.Int(nullable: false));
            
            DropColumn("dbo.Posts", "Id");
            AddColumn("dbo.Posts", "Id", c => c.Int(nullable: false, identity: true));

            AlterColumn("dbo.CommentVotes", "CommentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "PostId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.SectionPosts", new[] { "Section_Id", "Post_Id" });
            AddPrimaryKey("dbo.SubcommentVotes", new[] { "UserId", "SubcommentId" });
            AddPrimaryKey("dbo.Subcomments", "Id");
            AddPrimaryKey("dbo.PostVotes", new[] { "UserId", "PostId" });
            AddPrimaryKey("dbo.Posts", "Id");
            AddPrimaryKey("dbo.CommentVotes", new[] { "UserId", "CommentId" });
            AddPrimaryKey("dbo.Comments", "Id");
            CreateIndex("dbo.SectionPosts", "Post_Id");
            CreateIndex("dbo.SubcommentVotes", "SubcommentId");
            CreateIndex("dbo.Subcomments", "CommentId");
            CreateIndex("dbo.PostVotes", "PostId");
            CreateIndex("dbo.CommentVotes", "CommentId");
            CreateIndex("dbo.Comments", "PostId");
            AddForeignKey("dbo.SubcommentVotes", "SubcommentId", "dbo.Subcomments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostVotes", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SectionPosts", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subcomments", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CommentVotes", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
