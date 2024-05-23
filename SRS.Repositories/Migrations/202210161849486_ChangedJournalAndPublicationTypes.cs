namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangedJournalAndPublicationTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"update JournalTypes 
				   set PublicationType = (case when Id=1 then 6
				   when Id=2 then 7
				   when Id=3 then 7
				   when Id=4 then 7
				   when Id=5 then 8
				   when Id=6 then 9
				   when Id=7 then 10
                   else PublicationType
				   end);");
        }
        
        public override void Down()
        {
            Sql(@"update JournalTypes 
				   set PublicationType = (case when Id=1 then 5
				   when Id=2 then 6
				   when Id=3 then 6
				   when Id=4 then 6
				   when Id=5 then 7
				   when Id=6 then 8
				   when Id=7 then 9
                   else PublicationType
				   end);");
        }
    }
}
