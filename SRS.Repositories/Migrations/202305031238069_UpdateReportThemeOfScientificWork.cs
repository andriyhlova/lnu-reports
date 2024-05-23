namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReportThemeOfScientificWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportThemeOfScientificWorks", "Resume", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportThemeOfScientificWorks", "Resume");
        }
    }
}
