namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCathedraReportGrants : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CathedraReportReports", newName: "ReportCathedraReports");
            DropPrimaryKey("dbo.ReportCathedraReports");
            CreateTable(
                "dbo.CathedraReportThemeOfScientificWorks",
                c => new
                    {
                        CathedraReport_Id = c.Int(nullable: false),
                        ThemeOfScientificWork_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CathedraReport_Id, t.ThemeOfScientificWork_Id })
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWork_Id, cascadeDelete: true)
                .Index(t => t.CathedraReport_Id)
                .Index(t => t.ThemeOfScientificWork_Id);
            
            AddColumn("dbo.CathedraReports", "OtherGrants", c => c.String());
            AddPrimaryKey("dbo.ReportCathedraReports", new[] { "Report_Id", "CathedraReport_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CathedraReportThemeOfScientificWorks", "ThemeOfScientificWork_Id", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.CathedraReportThemeOfScientificWorks", "CathedraReport_Id", "dbo.CathedraReports");
            DropIndex("dbo.CathedraReportThemeOfScientificWorks", new[] { "ThemeOfScientificWork_Id" });
            DropIndex("dbo.CathedraReportThemeOfScientificWorks", new[] { "CathedraReport_Id" });
            DropPrimaryKey("dbo.ReportCathedraReports");
            DropColumn("dbo.CathedraReports", "OtherGrants");
            DropTable("dbo.CathedraReportThemeOfScientificWorks");
            AddPrimaryKey("dbo.ReportCathedraReports", new[] { "CathedraReport_Id", "Report_Id" });
            RenameTable(name: "dbo.ReportCathedraReports", newName: "CathedraReportReports");
        }
    }
}
