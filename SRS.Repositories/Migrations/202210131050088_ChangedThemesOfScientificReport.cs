namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangedThemesOfScientificReport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThemeOfScientificWorkReports", "ThemeOfScientificWork_Id", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.ThemeOfScientificWorkReports", "Report_Id", "dbo.Reports");
            DropIndex("dbo.ThemeOfScientificWorkReports", new[] { "ThemeOfScientificWork_Id" });
            DropIndex("dbo.ThemeOfScientificWorkReports", new[] { "Report_Id" });
            CreateTable(
                "dbo.ReportThemeOfScientificWorks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ReportId = c.Int(nullable: false),
                        ThemeOfScientificWorkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reports", t => t.ReportId, cascadeDelete: true)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWorkId, cascadeDelete: true)
                .Index(t => t.ReportId)
                .Index(t => t.ThemeOfScientificWorkId);

            Sql(@"INSERT INTO ReportThemeOfScientificWorks(ReportId, ThemeOfScientificWorkId, Description)
SELECT Report_Id, ThemeOfScientificWork_Id, (select ThemeOfScientificWorkDescription from Reports where Id=twr.Report_Id) 
FROM ThemeOfScientificWorkReports twr
join ThemeOfScientificWorks tw on twr.ThemeOfScientificWork_Id=tw.ID
where tw.Financial != 7;");
            Sql(@"INSERT INTO ReportThemeOfScientificWorks(ReportId, ThemeOfScientificWorkId, Description)
SELECT Report_Id, ThemeOfScientificWork_Id, (select ParticipationInGrands from Reports where Id=twr.Report_Id) 
FROM ThemeOfScientificWorkReports twr
join ThemeOfScientificWorks tw on twr.ThemeOfScientificWork_Id=tw.ID
where tw.Financial = 7;");
            
            DropColumn("dbo.Reports", "ParticipationInGrands");
            DropColumn("dbo.Reports", "ThemeOfScientificWorkDescription");
            DropTable("dbo.ThemeOfScientificWorkReports");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ThemeOfScientificWorkReports",
                c => new
                    {
                        ThemeOfScientificWork_Id = c.Int(nullable: false),
                        Report_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ThemeOfScientificWork_Id, t.Report_Id });
            
            AddColumn("dbo.Reports", "ThemeOfScientificWorkDescription", c => c.String());
            AddColumn("dbo.Reports", "ParticipationInGrands", c => c.String());

            Sql(@"INSERT INTO ThemeOfScientificWorkReports(Report_Id, ThemeOfScientificWork_Id)
SELECT ReportId, ThemeOfScientificWorkId FROM ReportThemeOfScientificWorks;");
            Sql(@"update Reports set Reports.ThemeOfScientificWorkDescription = rt.Description 
from Reports r inner join ReportThemeOfScientificWorks rt on r.ID = rt.ReportId
inner join ThemeOfScientificWorks tw on tw.ID = rt.ThemeOfScientificWorkId
where tw.Financial != 7;");
            Sql(@"update Reports set Reports.ParticipationInGrands = rt.Description 
from Reports r inner join ReportThemeOfScientificWorks rt on r.ID = rt.ReportId 
inner join ThemeOfScientificWorks tw on tw.ID = rt.ThemeOfScientificWorkId
where tw.Financial = 7;");

            DropForeignKey("dbo.ReportThemeOfScientificWorks", "ThemeOfScientificWorkId", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.ReportThemeOfScientificWorks", "ReportId", "dbo.Reports");
            DropIndex("dbo.ReportThemeOfScientificWorks", new[] { "ThemeOfScientificWorkId" });
            DropIndex("dbo.ReportThemeOfScientificWorks", new[] { "ReportId" });
            DropTable("dbo.ReportThemeOfScientificWorks");
            CreateIndex("dbo.ThemeOfScientificWorkReports", "Report_Id");
            CreateIndex("dbo.ThemeOfScientificWorkReports", "ThemeOfScientificWork_Id");
            AddForeignKey("dbo.ThemeOfScientificWorkReports", "Report_Id", "dbo.Reports", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ThemeOfScientificWorkReports", "ThemeOfScientificWork_Id", "dbo.ThemeOfScientificWorks", "Id", cascadeDelete: true);
        }
    }
}
