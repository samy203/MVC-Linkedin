namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class woohnitwit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friend", "FriendUserID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friend", "FriendUserID");
        }
    }
}
