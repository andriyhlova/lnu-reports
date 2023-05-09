namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThemeOfScientificWorkCathedra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThemeOfScientificWorkCathedras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CathedraId = c.Int(nullable: false),
                        ThemeOfScientificWorkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cathedras", t => t.CathedraId, cascadeDelete: true)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWorkId, cascadeDelete: true)
                .Index(t => t.CathedraId)
                .Index(t => t.ThemeOfScientificWorkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThemeOfScientificWorkCathedras", "ThemeOfScientificWorkId", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.ThemeOfScientificWorkCathedras", "CathedraId", "dbo.Cathedras");
            DropIndex("dbo.ThemeOfScientificWorkCathedras", new[] { "ThemeOfScientificWorkId" });
            DropIndex("dbo.ThemeOfScientificWorkCathedras", new[] { "CathedraId" });
            DropTable("dbo.ThemeOfScientificWorkCathedras");
        }
    }
}