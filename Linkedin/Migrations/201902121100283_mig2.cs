namespace Linkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Fk_ApplicationUserID = c.String(nullable: false, maxLength: 128),
                        Fk_PostID = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Fk_ApplicationUserID, t.Fk_PostID })
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_ApplicationUserID, cascadeDelete: true)
                .ForeignKey("dbo.Post", t => t.Fk_PostID, cascadeDelete: true)
                .Index(t => t.Fk_ApplicationUserID)
                .Index(t => t.Fk_PostID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.String(nullable: false, maxLength: 128),
                        Likes = c.Int(nullable: false),
                        Date = c.DateTime(),
                        Fk_ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_ApplicationUserID)
                .Index(t => t.Fk_ApplicationUserID);
            
            CreateTable(
                "dbo.Experience",
                c => new
                    {
                        ExperienceID = c.String(nullable: false, maxLength: 128),
                        StartYear = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Fk_ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ExperienceID)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_ApplicationUserID)
                .Index(t => t.Fk_ApplicationUserID);

            CreateTable(
                "dbo.Friend",
                c => new
                {
                    Fk_ApplicationUserID = c.String(nullable: false, maxLength: 128),
                    Fk_ApplicationFriendID = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.Fk_ApplicationUserID, t.Fk_ApplicationFriendID })
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_ApplicationUserID, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_ApplicationFriendID, cascadeDelete: false)
                .Index(t => t.Fk_ApplicationUserID)
                .Index(t => t.Fk_ApplicationFriendID);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageID = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                        Date = c.DateTime(),
                        Fk_SendingUserID = c.String(maxLength: 128),
                        Fk_RecievingUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_RecievingUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_SendingUserID)
                .Index(t => t.Fk_SendingUserID)
                .Index(t => t.Fk_RecievingUserID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Message", "Fk_SendingUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Message", "Fk_RecievingUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friend", "Fk_ApplicationFriendID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friend", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Experience", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment", "Fk_PostID", "dbo.Post");
            DropForeignKey("dbo.Post", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment", "Fk_ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Message", new[] { "Fk_RecievingUserID" });
            DropIndex("dbo.Message", new[] { "Fk_SendingUserID" });
            DropIndex("dbo.Friend", new[] { "Fk_ApplicationFriendID" });
            DropIndex("dbo.Friend", new[] { "Fk_ApplicationUserID" });
            DropIndex("dbo.Experience", new[] { "Fk_ApplicationUserID" });
            DropIndex("dbo.Post", new[] { "Fk_ApplicationUserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comment", new[] { "Fk_PostID" });
            DropIndex("dbo.Comment", new[] { "Fk_ApplicationUserID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Message");
            DropTable("dbo.Friend");
            DropTable("dbo.Experience");
            DropTable("dbo.Post");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comment");
        }
    }
}
