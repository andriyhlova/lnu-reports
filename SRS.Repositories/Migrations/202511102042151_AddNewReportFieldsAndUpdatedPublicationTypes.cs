namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewReportFieldsAndUpdatedPublicationTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"UPDATE Publications
                    SET PublicationType = CASE
                        WHEN PublicationType = 2  THEN 5
                        WHEN PublicationType = 3  THEN 6
                        WHEN PublicationType = 4  THEN 11
                        WHEN PublicationType = 5  THEN 12
                        WHEN PublicationType = 6  THEN 13
                        WHEN PublicationType = 7  THEN 14
                        WHEN PublicationType = 8  THEN 18
                        WHEN PublicationType = 9  THEN 19
                        WHEN PublicationType = 10 THEN 20
                        WHEN PublicationType = 11 THEN 21
                        WHEN PublicationType = 12 THEN 22
                        WHEN PublicationType = 13 THEN 23
                        WHEN PublicationType = 14 THEN 24
                        WHEN PublicationType = 15 THEN 25
                        ELSE PublicationType
                    END;");

            AddColumn("dbo.Reports", "ApplicationsForInternationGrantsHorizonEurope", c => c.String());
            AddColumn("dbo.Reports", "ApplicationsForInternationGrantsErasmus", c => c.String());
            AddColumn("dbo.Reports", "ApplicationsForGrantsOfOtherFunds", c => c.String());
            AddColumn("dbo.Reports", "ApplicationsForNationwideCompetitionsOfNRFU", c => c.String());
            AddColumn("dbo.Reports", "ApplicationsForOtherCompetitions", c => c.String());
            AddColumn("dbo.Reports", "ExpertiseInInternationalCompetitions", c => c.String());
            AddColumn("dbo.Reports", "ExpertiseInNationwideCompetitiveSelections", c => c.String());
        }
        
        public override void Down()
        {
            Sql(@"UPDATE Publications
                    SET PublicationType = CASE
                        WHEN PublicationType = 5  THEN 2
                        WHEN PublicationType = 6  THEN 3
                        WHEN PublicationType = 11  THEN 4
                        WHEN PublicationType = 12  THEN 5
                        WHEN PublicationType = 13  THEN 6
                        WHEN PublicationType = 14  THEN 7
                        WHEN PublicationType = 18  THEN 8
                        WHEN PublicationType = 19  THEN 9
                        WHEN PublicationType = 20 THEN 10
                        WHEN PublicationType = 21 THEN 11
                        WHEN PublicationType = 22 THEN 12
                        WHEN PublicationType = 23 THEN 13
                        WHEN PublicationType = 24 THEN 14
                        WHEN PublicationType = 25 THEN 15
                        ELSE PublicationType
                    END;");

            DropColumn("dbo.Reports", "ExpertiseInNationwideCompetitiveSelections");
            DropColumn("dbo.Reports", "ExpertiseInInternationalCompetitions");
            DropColumn("dbo.Reports", "ApplicationsForOtherCompetitions");
            DropColumn("dbo.Reports", "ApplicationsForNationwideCompetitionsOfNRFU");
            DropColumn("dbo.Reports", "ApplicationsForGrantsOfOtherFunds");
            DropColumn("dbo.Reports", "ApplicationsForInternationGrantsErasmus");
            DropColumn("dbo.Reports", "ApplicationsForInternationGrantsHorizonEurope");
        }
    }
}
