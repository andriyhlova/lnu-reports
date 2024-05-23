namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "Currency", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThemeOfScientificWorks", "Currency");
        }
    }
}
