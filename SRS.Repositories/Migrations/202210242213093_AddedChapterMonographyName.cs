namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedChapterMonographyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "ChapterMonographyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "ChapterMonographyName");
        }
    }
}
