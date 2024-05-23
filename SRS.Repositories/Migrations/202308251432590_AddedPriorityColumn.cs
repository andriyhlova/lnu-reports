namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPriorityColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcademicStatus", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.AcademicStatus", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Degrees", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Degrees", "Priority");
            DropColumn("dbo.AcademicStatus", "Type");
            DropColumn("dbo.AcademicStatus", "Priority");
        }
    }
}
