namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddInternationalDatabaseArticlePublicationTypesToJournalTypes : DbMigration
    {
        // 15 = Q1_Q2, 16 = Q3_Q4 (legacy), 17 = Q1, 18 = Q2, 19 = Q3, 20 = Q4, 21 = without quartile, 22+ = the rest (old values + 5).
        // The new types are available for the same journal types as their legacy types.
        public override void Up()
        {
            Sql(@"UPDATE Publications
                    SET PublicationType = PublicationType + 5
                    WHERE PublicationType >= 17;");

            Sql(@"UPDATE JournalTypePublicationTypes
                    SET PublicationType = PublicationType + 5
                    WHERE PublicationType >= 17;");

            Sql(@"INSERT INTO JournalTypePublicationTypes (PublicationType, JournalTypeId)
                    SELECT q.PublicationType, t.JournalTypeId
                    FROM JournalTypePublicationTypes t
                    INNER JOIN (SELECT 15 AS LegacyType, 17 AS PublicationType
                                UNION ALL SELECT 15, 18
                                UNION ALL SELECT 16, 19
                                UNION ALL SELECT 16, 20
                                UNION ALL SELECT 16, 21) q ON q.LegacyType = t.PublicationType;");
        }

        public override void Down()
        {
            Sql(@"DELETE FROM JournalTypePublicationTypes
                    WHERE PublicationType BETWEEN 17 AND 21;");

            Sql(@"UPDATE JournalTypePublicationTypes
                    SET PublicationType = PublicationType - 5
                    WHERE PublicationType >= 22;");

            Sql(@"UPDATE Publications
                    SET PublicationType = CASE
                        WHEN PublicationType IN (17, 18) THEN 15
                        ELSE 16
                    END
                    WHERE PublicationType BETWEEN 17 AND 21;");

            Sql(@"UPDATE Publications
                    SET PublicationType = PublicationType - 5
                    WHERE PublicationType >= 22;");
        }
    }
}
