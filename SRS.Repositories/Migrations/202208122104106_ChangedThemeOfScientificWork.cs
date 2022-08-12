namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedThemeOfScientificWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "SubCategory", c => c.Int());
            AddColumn("dbo.ThemeOfScientificWorks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ThemeOfScientificWorks", "UserId");
            AddForeignKey("dbo.ThemeOfScientificWorks", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThemeOfScientificWorks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ThemeOfScientificWorks", new[] { "UserId" });
            DropColumn("dbo.ThemeOfScientificWorks", "UserId");
            DropColumn("dbo.ThemeOfScientificWorks", "SubCategory");
        }
    }
}
