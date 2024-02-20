namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedThemeOfScientificWorkSupervisors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThemeOfScientificWorkSupervisors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupervisorId = c.String(maxLength: 128),
                        ThemeOfScientificWorkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SupervisorId)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWorkId, cascadeDelete: true)
                .Index(t => t.SupervisorId)
                .Index(t => t.ThemeOfScientificWorkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThemeOfScientificWorkSupervisors", "ThemeOfScientificWorkId", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.ThemeOfScientificWorkSupervisors", "SupervisorId", "dbo.AspNetUsers");
            DropIndex("dbo.ThemeOfScientificWorkSupervisors", new[] { "ThemeOfScientificWorkId" });
            DropIndex("dbo.ThemeOfScientificWorkSupervisors", new[] { "SupervisorId" });
            DropTable("dbo.ThemeOfScientificWorkSupervisors");
        }
    }
}
