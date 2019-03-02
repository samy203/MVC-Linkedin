namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nitwit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Comment", new[] { "Fk_ApplicationUserID" });
            DropPrimaryKey("dbo.Comment");
            AddColumn("dbo.Comment", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Comment", "Fk_ApplicationUserID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Comment", "Id");
            CreateIndex("dbo.Comment", "Fk_ApplicationUserID");
            AddForeignKey("dbo.Comment", "Fk_ApplicationUserID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
            DropForeignKey("dbo.Comment", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Comment", new[] { "Fk_ApplicationUserID" });
            DropPrimaryKey("dbo.Comment");
            AlterColumn("dbo.Comment", "Fk_ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Comment", "Id");
            AddPrimaryKey("dbo.Comment", new[] { "Fk_ApplicationUserID", "Fk_PostID" });
            CreateIndex("dbo.Comment", "Fk_ApplicationUserID");
            AddForeignKey("dbo.Comment", "Fk_ApplicationUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
