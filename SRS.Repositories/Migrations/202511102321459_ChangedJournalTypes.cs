namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedJournalTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JournalTypePublicationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicationType = c.Int(),
                        JournalTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JournalTypes", t => t.JournalTypeId, cascadeDelete: true)
                .Index(t => t.JournalTypeId);

            Sql(@"INSERT INTO JournalTypePublicationTypes(PublicationType, JournalTypeId)
                    VALUES 
                        (13,1), (15, 1), (16, 1),
                        (14,2), (15, 2), (16, 2),
                        (14,3), (15, 3), (16, 3),
                        (14,4), (15, 4), (16, 4),
                        (18,5), (19, 6), (19, 7), (20, 8);");

            DropColumn("dbo.JournalTypes", "PublicationType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JournalTypes", "PublicationType", c => c.Int());

            Sql(@"UPDATE JournalTypes
                    SET PublicationType = CASE
                        WHEN Id = 1 THEN 6
                        WHEN Id = 2 THEN 7
                        WHEN Id = 3 THEN 7
                        WHEN Id = 4 THEN 7
                        WHEN Id = 5 THEN 8
                        WHEN Id = 6 THEN 9
                        WHEN Id = 7 THEN 9
                        WHEN Id = 8 THEN 10
                        ELSE 0
                    END;");

            DropForeignKey("dbo.JournalTypePublicationTypes", "JournalTypeId", "dbo.JournalTypes");
            DropIndex("dbo.JournalTypePublicationTypes", new[] { "JournalTypeId" });
            DropTable("dbo.JournalTypePublicationTypes");
        }
    }
}
