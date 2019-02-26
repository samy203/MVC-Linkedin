namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nitwit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "Fk_PostID", "dbo.Post");
            DropPrimaryKey("dbo.Experience");
            DropPrimaryKey("dbo.Skill");
            DropPrimaryKey("dbo.Post");
            DropPrimaryKey("dbo.Message");
            AddColumn("dbo.Experience", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Skill", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Post", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Message", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Experience", "Id");
            AddPrimaryKey("dbo.Skill", "Id");
            AddPrimaryKey("dbo.Post", "Id");
            AddPrimaryKey("dbo.Message", "Id");
            AddForeignKey("dbo.Comment", "Fk_PostID", "dbo.Post", "Id", cascadeDelete: true);
            DropColumn("dbo.Experience", "ExperienceID");
            DropColumn("dbo.Skill", "SkillID");
            DropColumn("dbo.Post", "PostID");
            DropColumn("dbo.Message", "MessageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "MessageID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Post", "PostID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Skill", "SkillID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Experience", "ExperienceID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Comment", "Fk_PostID", "dbo.Post");
            DropPrimaryKey("dbo.Message");
            DropPrimaryKey("dbo.Post");
            DropPrimaryKey("dbo.Skill");
            DropPrimaryKey("dbo.Experience");
            DropColumn("dbo.Message", "Id");
            DropColumn("dbo.Post", "Id");
            DropColumn("dbo.Skill", "Id");
            DropColumn("dbo.Experience", "Id");
            AddPrimaryKey("dbo.Message", "MessageID");
            AddPrimaryKey("dbo.Post", "PostID");
            AddPrimaryKey("dbo.Skill", "SkillID");
            AddPrimaryKey("dbo.Experience", "ExperienceID");
            AddForeignKey("dbo.Comment", "Fk_PostID", "dbo.Post", "PostID", cascadeDelete: true);
        }
    }
}
