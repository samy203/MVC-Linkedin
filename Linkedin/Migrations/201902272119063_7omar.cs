namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7omar : DbMigration
    {
        public override void Up()
        {
//             DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
//             DropColumn("dbo.Friend", "Fk_ApplicationUserID");
//             RenameColumn(table: "dbo.Friend", name: "ApplicationUser_Id", newName: "Fk_ApplicationUserID");
//             DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Friend", name: "Fk_ApplicationUserID", newName: "ApplicationUser_Id");
            AddColumn("dbo.Friend", "Fk_ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "ApplicationUser_Id");
        }
    }
}
