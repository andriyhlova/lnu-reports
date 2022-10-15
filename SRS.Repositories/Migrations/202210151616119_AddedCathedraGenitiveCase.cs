namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCathedraGenitiveCase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cathedras", "GenitiveCase", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cathedras", "GenitiveCase");
        }
    }
}
