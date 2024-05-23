namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignedMultipleThemesToReport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reports", "ThemeOfScientificWork_Id", "dbo.ThemeOfScientificWorks");
            DropIndex("dbo.Reports", new[] { "ThemeOfScientificWork_Id" });
            CreateTable(
                "dbo.ThemeOfScientificWorkReports",
                c => new
                    {
                        ThemeOfScientificWork_Id = c.Int(nullable: false),
                        Report_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ThemeOfScientificWork_Id, t.Report_Id })
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWork_Id, cascadeDelete: true)
                .ForeignKey("dbo.Reports", t => t.Report_Id, cascadeDelete: true)
                .Index(t => t.ThemeOfScientificWork_Id)
                .Index(t => t.Report_Id);

            Sql(@"insert into dbo.ThemeOfScientificWorkReports(ThemeOfScientificWork_Id, Report_Id) 
                  select ThemeOfScientificWork_Id, Id from dbo.Reports where ThemeOfScientificWork_Id is not null");

            DropColumn("dbo.Reports", "ThemeOfScientificWork_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "ThemeOfScientificWork_Id", c => c.Int());

            Sql(@"update dbo.Reports set dbo.Reports.ThemeOfScientificWork_Id = tr.ThemeOfScientificWork_Id
                    from
                        dbo.Reports r
                    inner join
                        ThemeOfScientificWorkReports tr
                    on 
                        r.ID = tr.Report_Id;");

            DropForeignKey("dbo.ThemeOfScientificWorkReports", "Report_Id", "dbo.Reports");
            DropForeignKey("dbo.ThemeOfScientificWorkReports", "ThemeOfScientificWork_Id", "dbo.ThemeOfScientificWorks");
            DropIndex("dbo.ThemeOfScientificWorkReports", new[] { "Report_Id" });
            DropIndex("dbo.ThemeOfScientificWorkReports", new[] { "ThemeOfScientificWork_Id" });
            DropTable("dbo.ThemeOfScientificWorkReports");
            CreateIndex("dbo.Reports", "ThemeOfScientificWork_Id");
            AddForeignKey("dbo.Reports", "ThemeOfScientificWork_Id", "dbo.ThemeOfScientificWorks", "Id");
        }
    }
}
