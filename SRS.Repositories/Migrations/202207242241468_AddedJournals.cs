namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedJournals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journals",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    ShortName = c.String(),
                    PrintIssn = c.String(),
                    ElectronicIssn = c.String(),
                    BestQuartile = c.Int(),
                })
                .PrimaryKey(t => t.Id);
            RenameColumn("dbo.Publications", "Magazine", "OtherJournal");
            AddColumn("dbo.Publications", "JournalId", c => c.Int());
            CreateIndex("dbo.Publications", "JournalId");
            AddForeignKey("dbo.Publications", "JournalId", "dbo.Journals", "Id");
        }

        public override void Down()
        {
            RenameColumn("dbo.Publications", "OtherJournal", "Magazine");
            DropForeignKey("dbo.Publications", "JournalId", "dbo.Journals");
            DropIndex("dbo.Publications", new[] { "JournalId" });
            DropColumn("dbo.Publications", "JournalId");
            DropTable("dbo.Journals");
        }
    }
}
