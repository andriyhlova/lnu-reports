namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCathedra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cathedras", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cathedras", "Value");
        }
    }
}