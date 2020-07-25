namespace GameServerServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MainRedColor = c.Byte(nullable: false),
                        MainGreenColor = c.Byte(nullable: false),
                        MainBlueColor = c.Byte(nullable: false),
                        StrokeRedColor = c.Byte(nullable: false),
                        StrokeGreenColor = c.Byte(nullable: false),
                        StrokeBlueColor = c.Byte(nullable: false),
                        StrokeLength = c.Byte(nullable: false),
                        StrokeSpace = c.Byte(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        Nickname = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropIndex("dbo.Characters", new[] { "UserId" });
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.Characters");
        }
    }
}
