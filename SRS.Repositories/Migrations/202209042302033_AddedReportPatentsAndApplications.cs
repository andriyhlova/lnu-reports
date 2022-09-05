namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReportPatentsAndApplications : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ReportPublication1", newName: "ReportPublication3");
            Sql("exec sp_rename '[FK_dbo.ReportPublication1_dbo.Reports_Report_ID]', 'FK_dbo.ReportPublication3_dbo.Reports_Report_Id'; ");
            Sql("sp_rename '[FK_dbo.ReportPublication1_dbo.Publications_Publication_Id]', 'FK_dbo.ReportPublication3_dbo.Publications_Publication_Id'; ");
            //rename foreign key
            RenameTable(name: "dbo.ReportPublication2", newName: "ReportPublication4");
            Sql("sp_rename '[FK_dbo.ReportPublication2_dbo.Reports_Report_Id]', 'FK_dbo.ReportPublication4_dbo.Reports_Report_Id'; ");
            Sql("sp_rename '[FK_dbo.ReportPublication2_dbo.Publications_Publication_Id]', 'FK_dbo.ReportPublication4_dbo.Publications_Publication_Id'; ");
            CreateTable(
                "dbo.ReportPublication1",
                c => new
                    {
                        Report_Id = c.Int(nullable: false),
                        Publication_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_Id, t.Publication_Id })
                .ForeignKey("dbo.Reports", t => t.Report_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_Id, cascadeDelete: true)
                .Index(t => t.Report_Id)
                .Index(t => t.Publication_Id);
            
            CreateTable(
                "dbo.ReportPublication2",
                c => new
                    {
                        Report_Id = c.Int(nullable: false),
                        Publication_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_Id, t.Publication_Id })
                .ForeignKey("dbo.Reports", t => t.Report_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_Id, cascadeDelete: true)
                .Index(t => t.Report_Id)
                .Index(t => t.Publication_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportPublication2", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.ReportPublication2", "Report_Id", "dbo.Reports");
            DropForeignKey("dbo.ReportPublication1", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.ReportPublication1", "Report_Id", "dbo.Reports");
            DropIndex("dbo.ReportPublication2", new[] { "Publication_Id" });
            DropIndex("dbo.ReportPublication2", new[] { "Report_Id" });
            DropIndex("dbo.ReportPublication1", new[] { "Publication_Id" });
            DropIndex("dbo.ReportPublication1", new[] { "Report_Id" });
            DropTable("dbo.ReportPublication2");
            DropTable("dbo.ReportPublication1");
            RenameTable(name: "dbo.ReportPublication4", newName: "ReportPublication2");
            RenameTable(name: "dbo.ReportPublication3", newName: "ReportPublication1");
            Sql("sp_rename '[FK_dbo.ReportPublication4_dbo.Reports_Report_Id]', 'FK_dbo.ReportPublication2_dbo.Reports_Report_Id'; ");
            Sql("sp_rename '[FK_dbo.ReportPublication4_dbo.Publications_Publication_Id]', 'FK_dbo.ReportPublication2_dbo.Publications_Publication_Id'; ");
            Sql("sp_rename '[FK_dbo.ReportPublication3_dbo.Reports_Report_Id]', 'FK_dbo.ReportPublication1_dbo.Reports_Report_Id'; ");
            Sql("sp_rename '[FK_dbo.ReportPublication3_dbo.Publications_Publication_Id]', 'FK_dbo.ReportPublication1_dbo.Publications_Publication_Id'; ");
        }
    }
}
