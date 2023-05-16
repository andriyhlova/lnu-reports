namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReports : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportThemeOfScientificWorks", "Publications", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportThemeOfScientificWorks", "Publications");
        }
    }
}
