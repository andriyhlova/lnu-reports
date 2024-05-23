namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefendedDissertation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportThemeOfScientificWorks", "DefendedDissertation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportThemeOfScientificWorks", "DefendedDissertation");
        }
    }
}
