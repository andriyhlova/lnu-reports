namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEditorsField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "Editors", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "Editors");
        }
    }
}
