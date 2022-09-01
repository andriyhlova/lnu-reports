namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPublicationFieldsAndChangedTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"update Publications 
				   set PublicationType = (case when PublicationType=1 then 2
				   when PublicationType=2 then 3
				   when PublicationType=3 then 7
				   when PublicationType=5 then 10
				   when PublicationType=6 then 11
				   when PublicationType=7 then 13
				   when PublicationType=8 then 7
				   when PublicationType=9 then 8
				   when PublicationType=10 then 9
				   when PublicationType=11 then 6
				   when PublicationType=12 then 7
				   when PublicationType=13 then 5
                   else PublicationType
				   end);");
            AddColumn("dbo.Publications", "NumberOfPages", c => c.Int());
            AddColumn("dbo.Publications", "ISBN", c => c.String());
            AddColumn("dbo.Publications", "ConferenceName", c => c.String());
            AddColumn("dbo.Publications", "ConferenceDate", c => c.DateTime());
            AddColumn("dbo.Publications", "ConferenceCountry", c => c.String());
            AddColumn("dbo.Publications", "Issue", c => c.String());
            AddColumn("dbo.Publications", "ApplicationNumber", c => c.String());
            AddColumn("dbo.Publications", "ApplicationDate", c => c.DateTime());
            AddColumn("dbo.Publications", "ApplicationOwner", c => c.String());
            AddColumn("dbo.Publications", "BulletinNumber", c => c.String());
        }
        
        public override void Down()
        {
            Sql(@"update Publications 
				   set PublicationType = (case when PublicationType=2 then 1
				   when PublicationType=3 then 2
				   when PublicationType=7 then 3
				   when PublicationType=10 then 5
				   when PublicationType=11 then 6
				   when PublicationType=13 then 7
				   when PublicationType=7 then 8
				   when PublicationType=8 then 9
				   when PublicationType=9 then 10
				   when PublicationType=6 then 11
				   when PublicationType=7 then 12
				   when PublicationType=5 then 13
                   else PublicationType
				   end);");
            DropColumn("dbo.Publications", "BulletinNumber");
            DropColumn("dbo.Publications", "ApplicationOwner");
            DropColumn("dbo.Publications", "ApplicationDate");
            DropColumn("dbo.Publications", "ApplicationNumber");
            DropColumn("dbo.Publications", "Issue");
            DropColumn("dbo.Publications", "ConferenceCountry");
            DropColumn("dbo.Publications", "ConferenceDate");
            DropColumn("dbo.Publications", "ConferenceName");
            DropColumn("dbo.Publications", "ISBN");
            DropColumn("dbo.Publications", "NumberOfPages");
        }
    }
}
