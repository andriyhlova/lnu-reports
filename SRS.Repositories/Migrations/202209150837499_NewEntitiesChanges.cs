namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEntitiesChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JournalTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql(@"insert into JournalTypes(Value) values
				   (N'Видання, яке має імпакт-фактор'),
				   (N'Видання, яке включене до міжнародної наукометричної бази даних Web of Science'),
				   (N'Видання, яке включене до міжнародної наукометричної бази даних Scopus'),
				   (N'Видання, яке включене до іншої міжнародної наукометричної бази даних'),
				   (N'Інше закордонне видання'),
				   (N'Фахове видання України'),
				   (N'Інше видання України');");
            
            CreateTable(
                "dbo.JournalTypeJournals",
                c => new
                    {
                        JournalType_Id = c.Int(nullable: false),
                        Journal_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JournalType_Id, t.Journal_Id })
                .ForeignKey("dbo.JournalTypes", t => t.JournalType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Journals", t => t.Journal_Id, cascadeDelete: true)
                .Index(t => t.JournalType_Id)
                .Index(t => t.Journal_Id);
            
            AddColumn("dbo.ThemeOfScientificWorks", "OtherProjectType", c => c.String());
            Sql(@"update ThemeOfScientificWorks 
				   set Financial = (case when Financial=1 then 6
				   when Financial=2 then 8
				   when Financial=3 then 4
				   when Financial=4 then 3
				   when Financial=5 then 2
				   when Financial=6 then 5
                   else Financial
				   end);");
            AddColumn("dbo.Publications", "ConferenceEdition", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalTypeJournals", "Journal_Id", "dbo.Journals");
            DropForeignKey("dbo.JournalTypeJournals", "JournalType_Id", "dbo.JournalTypes");
            DropIndex("dbo.JournalTypeJournals", new[] { "Journal_Id" });
            DropIndex("dbo.JournalTypeJournals", new[] { "JournalType_Id" });
            DropColumn("dbo.Publications", "ConferenceEdition");
            DropColumn("dbo.ThemeOfScientificWorks", "OtherProjectType");
            Sql(@"update ThemeOfScientificWorks 
				   set Financial = (case when Financial=6 then 1
				   when Financial=8 then 2
				   when Financial=4 then 3
				   when Financial=3 then 4
				   when Financial=2 then 5
				   when Financial=5 then 6
                   else Financial
				   end);");
            DropTable("dbo.JournalTypeJournals");
            DropTable("dbo.JournalTypes");
        }
    }
}
