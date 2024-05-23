namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStudentPublications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportPublication5",
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
            DropForeignKey("dbo.ReportPublication5", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.ReportPublication5", "Report_Id", "dbo.Reports");
            DropIndex("dbo.ReportPublication5", new[] { "Publication_Id" });
            DropIndex("dbo.ReportPublication5", new[] { "Report_Id" });
            DropTable("dbo.ReportPublication5");
        }
    }
}
