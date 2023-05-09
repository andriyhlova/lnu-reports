namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCathedraValue : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cathedras", "Value");

        }
        
        public override void Down()
        {
            AddColumn("dbo.Cathedras", "Value", c => c.String());
        }
    }
}
