namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCathedraReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CathedraReportDissertationDefenses",
                c => new
                    {
                        CathedraReport_Id = c.Int(nullable: false),
                        DissertationDefense_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CathedraReport_Id, t.DissertationDefense_Id })
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.DissertationDefenses", t => t.DissertationDefense_Id, cascadeDelete: true)
                .Index(t => t.CathedraReport_Id)
                .Index(t => t.DissertationDefense_Id);
            
            CreateTable(
                "dbo.CathedraReportDissertationDefense1",
                c => new
                    {
                        CathedraReport_Id = c.Int(nullable: false),
                        DissertationDefense_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CathedraReport_Id, t.DissertationDefense_Id })
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.DissertationDefenses", t => t.DissertationDefense_Id, cascadeDelete: true)
                .Index(t => t.CathedraReport_Id)
                .Index(t => t.DissertationDefense_Id);
            
            CreateTable(
                "dbo.CathedraReportDissertationDefense2",
                c => new
                    {
                        CathedraReport_Id = c.Int(nullable: false),
                        DissertationDefense_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CathedraReport_Id, t.DissertationDefense_Id })
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.DissertationDefenses", t => t.DissertationDefense_Id, cascadeDelete: true)
                .Index(t => t.CathedraReport_Id)
                .Index(t => t.DissertationDefense_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CathedraReportDissertationDefense2", "DissertationDefense_Id", "dbo.DissertationDefenses");
            DropForeignKey("dbo.CathedraReportDissertationDefense2", "CathedraReport_Id", "dbo.CathedraReports");
            DropForeignKey("dbo.CathedraReportDissertationDefense1", "DissertationDefense_Id", "dbo.DissertationDefenses");
            DropForeignKey("dbo.CathedraReportDissertationDefense1", "CathedraReport_Id", "dbo.CathedraReports");
            DropForeignKey("dbo.CathedraReportDissertationDefenses", "DissertationDefense_Id", "dbo.DissertationDefenses");
            DropForeignKey("dbo.CathedraReportDissertationDefenses", "CathedraReport_Id", "dbo.CathedraReports");
            DropIndex("dbo.CathedraReportDissertationDefense2", new[] { "DissertationDefense_Id" });
            DropIndex("dbo.CathedraReportDissertationDefense2", new[] { "CathedraReport_Id" });
            DropIndex("dbo.CathedraReportDissertationDefense1", new[] { "DissertationDefense_Id" });
            DropIndex("dbo.CathedraReportDissertationDefense1", new[] { "CathedraReport_Id" });
            DropIndex("dbo.CathedraReportDissertationDefenses", new[] { "DissertationDefense_Id" });
            DropIndex("dbo.CathedraReportDissertationDefenses", new[] { "CathedraReport_Id" });
            DropTable("dbo.CathedraReportDissertationDefense2");
            DropTable("dbo.CathedraReportDissertationDefense1");
            DropTable("dbo.CathedraReportDissertationDefenses");
        }
    }
}
