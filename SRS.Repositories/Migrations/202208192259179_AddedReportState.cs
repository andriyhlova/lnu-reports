namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReportState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "State", c => c.Int(nullable: false));
            Sql(@"update dbo.Reports set State = (case when IsConfirmed = 1 then 2 when IsSigned = 1 then 1 else 0 end);");
            AddColumn("dbo.CathedraReports", "State", c => c.Int(nullable: false));
            DropColumn("dbo.Reports", "IsSigned");
            DropColumn("dbo.Reports", "IsConfirmed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "IsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reports", "IsSigned", c => c.Boolean(nullable: false));
            Sql(@"update dbo.Reports set IsConfirmed = (case when IsConfirmed = 1 then 1 else 0 end), IsSigned = (case when IsConfirmed = 1 or IsSigned = 1 then 1 else 0 end);");
            DropColumn("dbo.CathedraReports", "State");
            DropColumn("dbo.Reports", "State");
        }
    }
}
