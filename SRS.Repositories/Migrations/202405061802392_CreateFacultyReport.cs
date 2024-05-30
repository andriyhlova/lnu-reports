namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFacultyReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FacultyReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AchivementSchool = c.String(),
                        BudgetTheme_Id = c.Int(),
                        AllDescriptionBudgetTheme = c.String(),
                        CVBudgetTheme = c.String(),
                        DefensesOfCoworkersBudgetTheme = c.String(),
                        ApplicationAndPatentsOnInventionBudgetTheme = c.String(),
                        OtherBudgetTheme = c.String(),
                        ThemeInWorkTime_Id = c.Int(),
                        AllDescriptionThemeInWorkTime = c.String(),
                        CVThemeInWorkTime = c.String(),
                        DefensesOfCoworkersThemeInWorkTime = c.String(),
                        ApplicationAndPatentsOnInventionThemeInWorkTime = c.String(),
                        OtherThemeInWorkTime = c.String(),
                        HospDohovirTheme_Id = c.Int(),
                        AllDescriptionHospDohovirTheme = c.String(),
                        CVHospDohovirTheme = c.String(),
                        DefensesOfCoworkersHospDohovirTheme = c.String(),
                        ApplicationAndPatentsOnInventionHospDohovirTheme = c.String(),
                        OtherHospDohovirTheme = c.String(),
                        OtherFormsOfScientificWork = c.String(),
                        CooperationWithAcadamyOfScience = c.String(),
                        CooperationWithForeignScientificInstitution = c.String(),
                        StudentsWorks = c.String(),
                        ConferencesInUniversity = c.String(),
                        ApplicationOnInvention = c.String(),
                        Patents = c.String(),
                        Materials = c.String(),
                        PropositionForNewForms = c.String(),
                        Protocol = c.String(),
                        Date = c.DateTime(),
                        State = c.Int(nullable: false),
                        OtherGrants = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.BudgetTheme_Id)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.HospDohovirTheme_Id)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeInWorkTime_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.BudgetTheme_Id)
                .Index(t => t.ThemeInWorkTime_Id)
                .Index(t => t.HospDohovirTheme_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.FacultyReportPublications",
                c => new
                    {
                        FacultyReport_Id = c.Int(nullable: false),
                        Publication_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyReport_Id, t.Publication_Id })
                .ForeignKey("dbo.FacultyReports", t => t.FacultyReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_Id, cascadeDelete: true)
                .Index(t => t.FacultyReport_Id)
                .Index(t => t.Publication_Id);
            
            CreateTable(
                "dbo.FacultyReportDissertationDefenses",
                c => new
                    {
                        FacultyReport_Id = c.Int(nullable: false),
                        DissertationDefense_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyReport_Id, t.DissertationDefense_Id })
                .ForeignKey("dbo.FacultyReports", t => t.FacultyReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.DissertationDefenses", t => t.DissertationDefense_Id, cascadeDelete: true)
                .Index(t => t.FacultyReport_Id)
                .Index(t => t.DissertationDefense_Id);
            
            CreateTable(
                "dbo.FacultyReportDissertationDefense1",
                c => new
                    {
                        FacultyReport_Id = c.Int(nullable: false),
                        DissertationDefense_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyReport_Id, t.DissertationDefense_Id })
                .ForeignKey("dbo.FacultyReports", t => t.FacultyReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.DissertationDefenses", t => t.DissertationDefense_Id, cascadeDelete: true)
                .Index(t => t.FacultyReport_Id)
                .Index(t => t.DissertationDefense_Id);
            
            CreateTable(
                "dbo.FacultyReportDissertationDefense2",
                c => new
                    {
                        FacultyReport_Id = c.Int(nullable: false),
                        DissertationDefense_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyReport_Id, t.DissertationDefense_Id })
                .ForeignKey("dbo.FacultyReports", t => t.FacultyReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.DissertationDefenses", t => t.DissertationDefense_Id, cascadeDelete: true)
                .Index(t => t.FacultyReport_Id)
                .Index(t => t.DissertationDefense_Id);
            
            CreateTable(
                "dbo.FacultyReportThemeOfScientificWorks",
                c => new
                    {
                        FacultyReport_Id = c.Int(nullable: false),
                        ThemeOfScientificWork_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyReport_Id, t.ThemeOfScientificWork_Id })
                .ForeignKey("dbo.FacultyReports", t => t.FacultyReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWork_Id, cascadeDelete: true)
                .Index(t => t.FacultyReport_Id)
                .Index(t => t.ThemeOfScientificWork_Id);
            
            CreateTable(
                "dbo.FacultyReportPublication1",
                c => new
                    {
                        FacultyReport_Id = c.Int(nullable: false),
                        Publication_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyReport_Id, t.Publication_Id })
                .ForeignKey("dbo.FacultyReports", t => t.FacultyReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_Id, cascadeDelete: true)
                .Index(t => t.FacultyReport_Id)
                .Index(t => t.Publication_Id);
            
            CreateTable(
                "dbo.FacultyReportPublication2",
                c => new
                    {
                        FacultyReport_Id = c.Int(nullable: false),
                        Publication_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyReport_Id, t.Publication_Id })
                .ForeignKey("dbo.FacultyReports", t => t.FacultyReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_Id, cascadeDelete: true)
                .Index(t => t.FacultyReport_Id)
                .Index(t => t.Publication_Id);
            
            AddColumn("dbo.Reports", "FacultyReport_Id", c => c.Int());
            CreateIndex("dbo.Reports", "FacultyReport_Id");
            AddForeignKey("dbo.Reports", "FacultyReport_Id", "dbo.FacultyReports", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "FacultyReport_Id", "dbo.FacultyReports");
            DropForeignKey("dbo.FacultyReports", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FacultyReports", "ThemeInWorkTime_Id", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.FacultyReportPublication2", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.FacultyReportPublication2", "FacultyReport_Id", "dbo.FacultyReports");
            DropForeignKey("dbo.FacultyReportPublication1", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.FacultyReportPublication1", "FacultyReport_Id", "dbo.FacultyReports");
            DropForeignKey("dbo.FacultyReports", "HospDohovirTheme_Id", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.FacultyReportThemeOfScientificWorks", "ThemeOfScientificWork_Id", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.FacultyReportThemeOfScientificWorks", "FacultyReport_Id", "dbo.FacultyReports");
            DropForeignKey("dbo.FacultyReportDissertationDefense2", "DissertationDefense_Id", "dbo.DissertationDefenses");
            DropForeignKey("dbo.FacultyReportDissertationDefense2", "FacultyReport_Id", "dbo.FacultyReports");
            DropForeignKey("dbo.FacultyReportDissertationDefense1", "DissertationDefense_Id", "dbo.DissertationDefenses");
            DropForeignKey("dbo.FacultyReportDissertationDefense1", "FacultyReport_Id", "dbo.FacultyReports");
            DropForeignKey("dbo.FacultyReportDissertationDefenses", "DissertationDefense_Id", "dbo.DissertationDefenses");
            DropForeignKey("dbo.FacultyReportDissertationDefenses", "FacultyReport_Id", "dbo.FacultyReports");
            DropForeignKey("dbo.FacultyReports", "BudgetTheme_Id", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.FacultyReportPublications", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.FacultyReportPublications", "FacultyReport_Id", "dbo.FacultyReports");
            DropIndex("dbo.FacultyReportPublication2", new[] { "Publication_Id" });
            DropIndex("dbo.FacultyReportPublication2", new[] { "FacultyReport_Id" });
            DropIndex("dbo.FacultyReportPublication1", new[] { "Publication_Id" });
            DropIndex("dbo.FacultyReportPublication1", new[] { "FacultyReport_Id" });
            DropIndex("dbo.FacultyReportThemeOfScientificWorks", new[] { "ThemeOfScientificWork_Id" });
            DropIndex("dbo.FacultyReportThemeOfScientificWorks", new[] { "FacultyReport_Id" });
            DropIndex("dbo.FacultyReportDissertationDefense2", new[] { "DissertationDefense_Id" });
            DropIndex("dbo.FacultyReportDissertationDefense2", new[] { "FacultyReport_Id" });
            DropIndex("dbo.FacultyReportDissertationDefense1", new[] { "DissertationDefense_Id" });
            DropIndex("dbo.FacultyReportDissertationDefense1", new[] { "FacultyReport_Id" });
            DropIndex("dbo.FacultyReportDissertationDefenses", new[] { "DissertationDefense_Id" });
            DropIndex("dbo.FacultyReportDissertationDefenses", new[] { "FacultyReport_Id" });
            DropIndex("dbo.FacultyReportPublications", new[] { "Publication_Id" });
            DropIndex("dbo.FacultyReportPublications", new[] { "FacultyReport_Id" });
            DropIndex("dbo.FacultyReports", new[] { "User_Id" });
            DropIndex("dbo.FacultyReports", new[] { "HospDohovirTheme_Id" });
            DropIndex("dbo.FacultyReports", new[] { "ThemeInWorkTime_Id" });
            DropIndex("dbo.FacultyReports", new[] { "BudgetTheme_Id" });
            DropIndex("dbo.Reports", new[] { "FacultyReport_Id" });
            DropColumn("dbo.Reports", "FacultyReport_Id");
            DropTable("dbo.FacultyReportPublication2");
            DropTable("dbo.FacultyReportPublication1");
            DropTable("dbo.FacultyReportThemeOfScientificWorks");
            DropTable("dbo.FacultyReportDissertationDefense2");
            DropTable("dbo.FacultyReportDissertationDefense1");
            DropTable("dbo.FacultyReportDissertationDefenses");
            DropTable("dbo.FacultyReportPublications");
            DropTable("dbo.FacultyReports");
        }
    }
}
