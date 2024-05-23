namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedOtherThemeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "OtherThemeOfScientificWorkDescription", c => c.String());
            Sql(@"update Publications set Language=6 where Language = 7;");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "OtherThemeOfScientificWorkDescription");
            Sql(@"update Publications set Language=7 where Language = 6;");
        }
    }
}
