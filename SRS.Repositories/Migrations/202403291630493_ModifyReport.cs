namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportThemeOfScientificWorkApplicationUsers",
                c => new
                    {
                        ReportThemeOfScientificWork_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ReportThemeOfScientificWork_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.ReportThemeOfScientificWorks", t => t.ReportThemeOfScientificWork_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.ReportThemeOfScientificWork_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ReportThemeOfScientificWorkApplicationUser1",
                c => new
                    {
                        ReportThemeOfScientificWork_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ReportThemeOfScientificWork_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.ReportThemeOfScientificWorks", t => t.ReportThemeOfScientificWork_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.ReportThemeOfScientificWork_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ReportThemeOfScientificWorkApplicationUser2",
                c => new
                    {
                        ReportThemeOfScientificWork_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ReportThemeOfScientificWork_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.ReportThemeOfScientificWorks", t => t.ReportThemeOfScientificWork_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.ReportThemeOfScientificWork_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportThemeOfScientificWorkApplicationUser2", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReportThemeOfScientificWorkApplicationUser2", "ReportThemeOfScientificWork_Id", "dbo.ReportThemeOfScientificWorks");
            DropForeignKey("dbo.ReportThemeOfScientificWorkApplicationUser1", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReportThemeOfScientificWorkApplicationUser1", "ReportThemeOfScientificWork_Id", "dbo.ReportThemeOfScientificWorks");
            DropForeignKey("dbo.ReportThemeOfScientificWorkApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReportThemeOfScientificWorkApplicationUsers", "ReportThemeOfScientificWork_Id", "dbo.ReportThemeOfScientificWorks");
            DropIndex("dbo.ReportThemeOfScientificWorkApplicationUser2", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ReportThemeOfScientificWorkApplicationUser2", new[] { "ReportThemeOfScientificWork_Id" });
            DropIndex("dbo.ReportThemeOfScientificWorkApplicationUser1", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ReportThemeOfScientificWorkApplicationUser1", new[] { "ReportThemeOfScientificWork_Id" });
            DropIndex("dbo.ReportThemeOfScientificWorkApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ReportThemeOfScientificWorkApplicationUsers", new[] { "ReportThemeOfScientificWork_Id" });
            DropTable("dbo.ReportThemeOfScientificWorkApplicationUser2");
            DropTable("dbo.ReportThemeOfScientificWorkApplicationUser1");
            DropTable("dbo.ReportThemeOfScientificWorkApplicationUsers");
        }
    }
}
