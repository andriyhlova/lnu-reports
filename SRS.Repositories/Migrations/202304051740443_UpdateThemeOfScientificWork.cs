namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateThemeOfScientificWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "SupervisorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ThemeOfScientificWorks", "SupervisorId");
            AddForeignKey("dbo.ThemeOfScientificWorks", "SupervisorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThemeOfScientificWorks", "SupervisorId", "dbo.AspNetUsers");
            DropIndex("dbo.ThemeOfScientificWorks", new[] { "SupervisorId" });
            DropColumn("dbo.ThemeOfScientificWorks", "SupervisorId");
        }
    }
}
