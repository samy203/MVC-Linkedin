namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nitwit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friend", "Fk_ApplicationFriendID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friend", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Friend", new[] { "Fk_ApplicationUserID" });
            DropIndex("dbo.Friend", new[] { "Fk_ApplicationFriendID" });
            DropPrimaryKey("dbo.Friend");
            AddColumn("dbo.Friend", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Friend", "Fk_ApplicationUserID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Friend", "Id");
            CreateIndex("dbo.Friend", "Fk_ApplicationUserID");
            AddForeignKey("dbo.Friend", "Fk_ApplicationUserID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Friend", "Fk_ApplicationFriendID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friend", "Fk_ApplicationFriendID", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Friend", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Friend", new[] { "Fk_ApplicationUserID" });
            DropPrimaryKey("dbo.Friend");
            AlterColumn("dbo.Friend", "Fk_ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Friend", "Id");
            AddPrimaryKey("dbo.Friend", new[] { "Fk_ApplicationUserID", "Fk_ApplicationFriendID" });
            CreateIndex("dbo.Friend", "Fk_ApplicationFriendID");
            CreateIndex("dbo.Friend", "Fk_ApplicationUserID");
            AddForeignKey("dbo.Friend", "Fk_ApplicationUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Friend", "Fk_ApplicationFriendID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
