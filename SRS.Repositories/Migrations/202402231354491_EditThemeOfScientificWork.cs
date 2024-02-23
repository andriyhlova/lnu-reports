namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditThemeOfScientificWork : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThemeOfScientificWorks", "SupervisorId", "dbo.AspNetUsers");
            DropIndex("dbo.ThemeOfScientificWorks", new[] { "SupervisorId" });
            DropColumn("dbo.ThemeOfScientificWorks", "SupervisorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "SupervisorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ThemeOfScientificWorks", "SupervisorId");
            AddForeignKey("dbo.ThemeOfScientificWorks", "SupervisorId", "dbo.AspNetUsers", "Id");
        }
    }
}
