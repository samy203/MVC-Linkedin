namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nit5ara : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Skill", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Skill", "Fk_ApplicationUserID");
            RenameColumn(table: "dbo.Skill", name: "ApplicationUser_Id", newName: "Fk_ApplicationUserID");
            AlterColumn("dbo.Skill", "Fk_ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Skill", "Fk_ApplicationUserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Skill", new[] { "Fk_ApplicationUserID" });
            AlterColumn("dbo.Skill", "Fk_ApplicationUserID", c => c.String());
            RenameColumn(table: "dbo.Skill", name: "Fk_ApplicationUserID", newName: "ApplicationUser_Id");
            AddColumn("dbo.Skill", "Fk_ApplicationUserID", c => c.String());
            CreateIndex("dbo.Skill", "ApplicationUser_Id");
        }
    }
}
