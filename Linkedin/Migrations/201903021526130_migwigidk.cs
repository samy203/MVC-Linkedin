namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migwigidk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fk_PostID = c.Int(nullable: false),
                        Fk_ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_ApplicationUserID)
                .ForeignKey("dbo.Post", t => t.Fk_PostID, cascadeDelete: true)
                .Index(t => t.Fk_PostID)
                .Index(t => t.Fk_ApplicationUserID);
            
            DropColumn("dbo.Post", "Likes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post", "Likes", c => c.Int(nullable: false));
            DropForeignKey("dbo.Like", "Fk_PostID", "dbo.Post");
            DropForeignKey("dbo.Like", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Like", new[] { "Fk_ApplicationUserID" });
            DropIndex("dbo.Like", new[] { "Fk_PostID" });
            DropTable("dbo.Like");
        }
    }
}
