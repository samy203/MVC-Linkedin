namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        SkillID = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false),
                        Fk_ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SkillID)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_ApplicationUserID)
                .Index(t => t.Fk_ApplicationUserID);
            
            AddColumn("dbo.Comment", "Content", c => c.String(nullable: false));
            AddColumn("dbo.Post", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Message", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skill", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Skill", new[] { "Fk_ApplicationUserID" });
            AlterColumn("dbo.Message", "Content", c => c.String());
            DropColumn("dbo.Post", "Content");
            DropColumn("dbo.Comment", "Content");
            DropTable("dbo.Skill");
        }
    }
}
