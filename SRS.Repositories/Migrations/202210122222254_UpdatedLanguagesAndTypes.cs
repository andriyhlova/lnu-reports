namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLanguagesAndTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "OtherLanguage", c => c.String());
            Sql("update Publications set Language=16 where Language in (3,6,8,9,10,11,12,13,14);");
            Sql(@"update Publications set Language=(case when Language=4 then 3
                                                         when Language=5 then 4
                                                         when Language=7 then 5
                                                         when Language=15 then 6
                                                         else Language end);");
            Sql(@"update Publications set PublicationType = PublicationType + 1 where PublicationType > 2;");
        }
        
        public override void Down()
        {
            Sql(@"update Publications set PublicationType = PublicationType - 1 where PublicationType > 3");
            Sql(@"update Publications set Language=(case when Language=3 then 4
                                                         when Language=4 then 5
                                                         when Language=5 then 7
                                                         when Language=6 then 15
                                                         else Language end);");
            DropColumn("dbo.Publications", "OtherLanguage");
        }
    }
}
