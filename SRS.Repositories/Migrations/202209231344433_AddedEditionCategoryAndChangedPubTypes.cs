namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedEditionCategoryAndChangedPubTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"update Publications 
				   set PublicationType = (case when PublicationType=4 then 13
				   when PublicationType=5 then 4
				   when PublicationType=6 then 5
				   when PublicationType=7 then 6
				   when PublicationType=8 then 7
				   when PublicationType=9 then 8
				   when PublicationType=10 then 9
				   when PublicationType=11 then 10
				   when PublicationType=12 then 11
				   when PublicationType=13 then 12
                   else PublicationType
				   end);");
            AddColumn("dbo.Publications", "EditionCategory", c => c.Int());
        }
        
        public override void Down()
        {
			Sql(@"update Publications 
				   set PublicationType = (case when PublicationType=13 then 4
				   when PublicationType=4 then 5
				   when PublicationType=5 then 6
				   when PublicationType=6 then 7
				   when PublicationType=7 then 8
				   when PublicationType=8 then 9
				   when PublicationType=9 then 19
				   when PublicationType=10 then 11
				   when PublicationType=11 then 12
				   when PublicationType=12 then 13
                   else PublicationType
				   end);");
			DropColumn("dbo.Publications", "EditionCategory");
        }
    }
}
