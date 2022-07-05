namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBaseEntity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cathedras", new[] { "Faculty_ID" });
            DropIndex("dbo.ThemeOfScientificWorks", new[] { "Cathedra_ID" });
            DropIndex("dbo.Reports", new[] { "ThemeOfScientificWork_ID" });
            DropIndex("dbo.CathedraReports", new[] { "BudgetTheme_ID" });
            DropIndex("dbo.CathedraReports", new[] { "HospDohovirTheme_ID" });
            DropIndex("dbo.CathedraReports", new[] { "ThemeInWorkTime_ID" });
            DropIndex("dbo.CoworkersDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.OtherDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "AcademicStatus_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Cathedra_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Position_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "ScienceDegree_ID" });
            DropIndex("dbo.CathedraReportPublications", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReportPublications", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportPublication1", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReportPublication1", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportPublication2", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReportPublication2", new[] { "Publication_ID" });
            DropIndex("dbo.ApplicationUserPublications", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportReports", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReportReports", new[] { "Report_ID" });
            DropIndex("dbo.ReportPublications", new[] { "Report_ID" });
            DropIndex("dbo.ReportPublications", new[] { "Publication_ID" });
            DropIndex("dbo.ReportPublication1", new[] { "Report_ID" });
            DropIndex("dbo.ReportPublication1", new[] { "Publication_ID" });
            DropIndex("dbo.ReportPublication2", new[] { "Report_ID" });
            DropIndex("dbo.ReportPublication2", new[] { "Publication_ID" });
            CreateIndex("dbo.Cathedras", "Faculty_Id");
            CreateIndex("dbo.ThemeOfScientificWorks", "Cathedra_Id");
            CreateIndex("dbo.Reports", "ThemeOfScientificWork_Id");
            CreateIndex("dbo.CathedraReports", "BudgetTheme_Id");
            CreateIndex("dbo.CathedraReports", "HospDohovirTheme_Id");
            CreateIndex("dbo.CathedraReports", "ThemeInWorkTime_Id");
            CreateIndex("dbo.CoworkersDefenses", "CathedraReport_Id");
            CreateIndex("dbo.CathedraDefenses", "CathedraReport_Id");
            CreateIndex("dbo.OtherDefenses", "CathedraReport_Id");
            CreateIndex("dbo.AspNetUsers", "AcademicStatus_Id");
            CreateIndex("dbo.AspNetUsers", "Cathedra_Id");
            CreateIndex("dbo.AspNetUsers", "Position_Id");
            CreateIndex("dbo.AspNetUsers", "ScienceDegree_Id");
            CreateIndex("dbo.CathedraReportPublications", "CathedraReport_Id");
            CreateIndex("dbo.CathedraReportPublications", "Publication_Id");
            CreateIndex("dbo.CathedraReportPublication1", "CathedraReport_Id");
            CreateIndex("dbo.CathedraReportPublication1", "Publication_Id");
            CreateIndex("dbo.CathedraReportPublication2", "CathedraReport_Id");
            CreateIndex("dbo.CathedraReportPublication2", "Publication_Id");
            CreateIndex("dbo.ApplicationUserPublications", "Publication_Id");
            CreateIndex("dbo.CathedraReportReports", "CathedraReport_Id");
            CreateIndex("dbo.CathedraReportReports", "Report_Id");
            CreateIndex("dbo.ReportPublications", "Report_Id");
            CreateIndex("dbo.ReportPublications", "Publication_Id");
            CreateIndex("dbo.ReportPublication1", "Report_Id");
            CreateIndex("dbo.ReportPublication1", "Publication_Id");
            CreateIndex("dbo.ReportPublication2", "Report_Id");
            CreateIndex("dbo.ReportPublication2", "Publication_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReportPublication2", new[] { "Publication_Id" });
            DropIndex("dbo.ReportPublication2", new[] { "Report_Id" });
            DropIndex("dbo.ReportPublication1", new[] { "Publication_Id" });
            DropIndex("dbo.ReportPublication1", new[] { "Report_Id" });
            DropIndex("dbo.ReportPublications", new[] { "Publication_Id" });
            DropIndex("dbo.ReportPublications", new[] { "Report_Id" });
            DropIndex("dbo.CathedraReportReports", new[] { "Report_Id" });
            DropIndex("dbo.CathedraReportReports", new[] { "CathedraReport_Id" });
            DropIndex("dbo.ApplicationUserPublications", new[] { "Publication_Id" });
            DropIndex("dbo.CathedraReportPublication2", new[] { "Publication_Id" });
            DropIndex("dbo.CathedraReportPublication2", new[] { "CathedraReport_Id" });
            DropIndex("dbo.CathedraReportPublication1", new[] { "Publication_Id" });
            DropIndex("dbo.CathedraReportPublication1", new[] { "CathedraReport_Id" });
            DropIndex("dbo.CathedraReportPublications", new[] { "Publication_Id" });
            DropIndex("dbo.CathedraReportPublications", new[] { "CathedraReport_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "ScienceDegree_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Position_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cathedra_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "AcademicStatus_Id" });
            DropIndex("dbo.OtherDefenses", new[] { "CathedraReport_Id" });
            DropIndex("dbo.CathedraDefenses", new[] { "CathedraReport_Id" });
            DropIndex("dbo.CoworkersDefenses", new[] { "CathedraReport_Id" });
            DropIndex("dbo.CathedraReports", new[] { "ThemeInWorkTime_Id" });
            DropIndex("dbo.CathedraReports", new[] { "HospDohovirTheme_Id" });
            DropIndex("dbo.CathedraReports", new[] { "BudgetTheme_Id" });
            DropIndex("dbo.Reports", new[] { "ThemeOfScientificWork_Id" });
            DropIndex("dbo.ThemeOfScientificWorks", new[] { "Cathedra_Id" });
            DropIndex("dbo.Cathedras", new[] { "Faculty_Id" });
            CreateIndex("dbo.ReportPublication2", "Publication_ID");
            CreateIndex("dbo.ReportPublication2", "Report_ID");
            CreateIndex("dbo.ReportPublication1", "Publication_ID");
            CreateIndex("dbo.ReportPublication1", "Report_ID");
            CreateIndex("dbo.ReportPublications", "Publication_ID");
            CreateIndex("dbo.ReportPublications", "Report_ID");
            CreateIndex("dbo.CathedraReportReports", "Report_ID");
            CreateIndex("dbo.CathedraReportReports", "CathedraReport_ID");
            CreateIndex("dbo.ApplicationUserPublications", "Publication_ID");
            CreateIndex("dbo.CathedraReportPublication2", "Publication_ID");
            CreateIndex("dbo.CathedraReportPublication2", "CathedraReport_ID");
            CreateIndex("dbo.CathedraReportPublication1", "Publication_ID");
            CreateIndex("dbo.CathedraReportPublication1", "CathedraReport_ID");
            CreateIndex("dbo.CathedraReportPublications", "Publication_ID");
            CreateIndex("dbo.CathedraReportPublications", "CathedraReport_ID");
            CreateIndex("dbo.AspNetUsers", "ScienceDegree_ID");
            CreateIndex("dbo.AspNetUsers", "Position_ID");
            CreateIndex("dbo.AspNetUsers", "Cathedra_ID");
            CreateIndex("dbo.AspNetUsers", "AcademicStatus_ID");
            CreateIndex("dbo.OtherDefenses", "CathedraReport_ID");
            CreateIndex("dbo.CathedraDefenses", "CathedraReport_ID");
            CreateIndex("dbo.CoworkersDefenses", "CathedraReport_ID");
            CreateIndex("dbo.CathedraReports", "ThemeInWorkTime_ID");
            CreateIndex("dbo.CathedraReports", "HospDohovirTheme_ID");
            CreateIndex("dbo.CathedraReports", "BudgetTheme_ID");
            CreateIndex("dbo.Reports", "ThemeOfScientificWork_ID");
            CreateIndex("dbo.ThemeOfScientificWorks", "Cathedra_ID");
            CreateIndex("dbo.Cathedras", "Faculty_ID");
        }
    }
}
