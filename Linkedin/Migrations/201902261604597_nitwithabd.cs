namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nitwithabd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skill", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Skill", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Skill", "Fk_ApplicationUserID", c => c.String(maxLength: 128));
            DropColumn("dbo.Skill", "ApplicationUser_Id");
            CreateIndex("dbo.Skill", "Fk_ApplicationUserID");
            AddForeignKey("dbo.Skill", "Fk_ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
           
        }
    }
}
