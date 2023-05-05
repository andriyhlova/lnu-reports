namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateJournalType : DbMigration
    {
        public override void Up()
        {
            Sql(@"UPDATE JournalTypes
                SET Value = N'Фахове видання України категорії А'
                WHERE Id = 6;");
            Sql(@"UPDATE JournalTypes
                SET Value = N'Фахове видання України категорії Б', PublicationType = 9
                WHERE Id = 7;");
            Sql(@"DBCC CHECKIDENT ('JournalTypes', RESEED, 7);
                INSERT INTO JournalTypes (Value, PublicationType)
                VALUES (N'Інше видання України', 10)");
            Sql(@"UPDATE JournalTypeJournals
                SET JournalType_Id = 8
                WHERE JournalType_Id = 7;");
            Sql(@"UPDATE JournalTypeJournals
                SET JournalType_Id = 7
                WHERE JournalType_Id = 6;");
        }
        
        public override void Down()
        {
            Sql(@"UPDATE JournalTypes
                SET Value = N'Фахове видання України'
                WHERE Id = 6;");
            Sql(@"UPDATE JournalTypes
                SET Value = N'Інше видання України'
                WHERE Id = 7;");
            Sql(@"DELETE FROM JournalTypes
                WHERE Id = 8;");
            Sql(@"UPDATE JournalTypeJournals
                SET JournalType_Id = 7
                WHERE JournalType_Id = 8;");
            Sql(@"UPDATE JournalTypeJournals
                SET JournalType_Id = 6
                WHERE JournalType_Id = 7;");
        }
    }
}