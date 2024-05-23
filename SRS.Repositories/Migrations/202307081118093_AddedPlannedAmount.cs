namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPlannedAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "PlannedAmount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ThemeOfScientificWorkFinancials", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ThemeOfScientificWorkFinancials", "Amount", c => c.Double(nullable: false));
            DropColumn("dbo.ThemeOfScientificWorks", "PlannedAmount");
        }
    }
}
