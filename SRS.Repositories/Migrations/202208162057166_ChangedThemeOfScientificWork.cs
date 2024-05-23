namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedThemeOfScientificWork : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThemeOfScientificWorkFinancials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        ThemeOfScientificWorkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWorkId, cascadeDelete: true)
                .Index(t => t.ThemeOfScientificWorkId);
            
            AddColumn("dbo.ThemeOfScientificWorks", "SubCategory", c => c.Int());
            AddColumn("dbo.ThemeOfScientificWorks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ThemeOfScientificWorks", "UserId");
            AddForeignKey("dbo.ThemeOfScientificWorks", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThemeOfScientificWorks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ThemeOfScientificWorkFinancials", "ThemeOfScientificWorkId", "dbo.ThemeOfScientificWorks");
            DropIndex("dbo.ThemeOfScientificWorkFinancials", new[] { "ThemeOfScientificWorkId" });
            DropIndex("dbo.ThemeOfScientificWorks", new[] { "UserId" });
            DropColumn("dbo.ThemeOfScientificWorks", "UserId");
            DropColumn("dbo.ThemeOfScientificWorks", "SubCategory");
            DropTable("dbo.ThemeOfScientificWorkFinancials");
        }
    }
}
