namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Description");
        }
    }
}
