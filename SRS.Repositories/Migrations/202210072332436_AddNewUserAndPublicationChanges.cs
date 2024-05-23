namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddNewUserAndPublicationChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HonoraryTitleApplicationUsers", "HonoraryTitle_Id", "dbo.HonoraryTitles");
            DropForeignKey("dbo.HonoraryTitleApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.HonoraryTitleApplicationUsers", new[] { "HonoraryTitle_Id" });
            DropIndex("dbo.HonoraryTitleApplicationUsers", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.ApplicationUserHonoraryTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AwardDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        HonoraryTitleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HonoraryTitles", t => t.HonoraryTitleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HonoraryTitleId);
            
            AddColumn("dbo.AcademicStatus", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Publications", "PublicationIdentifier", c => c.Int());
            AddColumn("dbo.JournalTypes", "PublicationType", c => c.Int());
            AddColumn("dbo.AspNetUsers", "InternationalMetricPublicationCounterBeforeRegistration", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ResearcherId", c => c.String());
            AddColumn("dbo.AspNetUsers", "Orcid", c => c.String());
            AddColumn("dbo.AspNetUsers", "ScopusAuthorId", c => c.String());
            AddColumn("dbo.AspNetUsers", "GoogleScholarLink", c => c.String());
            AddColumn("dbo.AspNetUsers", "ScopusHIndex", c => c.Int());
            AddColumn("dbo.AspNetUsers", "WebOfScienceHIndex", c => c.Int());
            AddColumn("dbo.AspNetUsers", "GoogleScholarHIndex", c => c.Int());
            AddColumn("dbo.AspNetUsers", "HonoraryTitle_Id", c => c.Int());
            AddColumn("dbo.Degrees", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.HonoraryTitles", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Positions", "SortOrder", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "HonoraryTitle_Id");
            AddForeignKey("dbo.AspNetUsers", "HonoraryTitle_Id", "dbo.HonoraryTitles", "Id");
            DropColumn("dbo.Publications", "EditionCategory");
            DropColumn("dbo.AspNetUsers", "AwardingDate");
            DropTable("dbo.HonoraryTitleApplicationUsers");
            Sql(@"update Publications set PublicationType = PublicationType + 1 where PublicationType > 0;");
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
        
        public override void Down()
        {
            CreateTable(
                "dbo.HonoraryTitleApplicationUsers",
                c => new
                    {
                        HonoraryTitle_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.HonoraryTitle_Id, t.ApplicationUser_Id });
            
            AddColumn("dbo.AspNetUsers", "AwardingDate", c => c.DateTime());
            AddColumn("dbo.Publications", "EditionCategory", c => c.Int());
            DropForeignKey("dbo.ApplicationUserHonoraryTitles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserHonoraryTitles", "HonoraryTitleId", "dbo.HonoraryTitles");
            DropForeignKey("dbo.AspNetUsers", "HonoraryTitle_Id", "dbo.HonoraryTitles");
            DropIndex("dbo.ApplicationUserHonoraryTitles", new[] { "HonoraryTitleId" });
            DropIndex("dbo.ApplicationUserHonoraryTitles", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "HonoraryTitle_Id" });
            DropColumn("dbo.Positions", "SortOrder");
            DropColumn("dbo.HonoraryTitles", "SortOrder");
            DropColumn("dbo.Degrees", "SortOrder");
            DropColumn("dbo.AspNetUsers", "HonoraryTitle_Id");
            DropColumn("dbo.AspNetUsers", "GoogleScholarHIndex");
            DropColumn("dbo.AspNetUsers", "WebOfScienceHIndex");
            DropColumn("dbo.AspNetUsers", "ScopusHIndex");
            DropColumn("dbo.AspNetUsers", "GoogleScholarLink");
            DropColumn("dbo.AspNetUsers", "ScopusAuthorId");
            DropColumn("dbo.AspNetUsers", "Orcid");
            DropColumn("dbo.AspNetUsers", "ResearcherId");
            DropColumn("dbo.AspNetUsers", "InternationalMetricPublicationCounterBeforeRegistration");
            DropColumn("dbo.JournalTypes", "PublicationType");
            DropColumn("dbo.Publications", "PublicationIdentifier");
            DropColumn("dbo.AcademicStatus", "SortOrder");
            DropTable("dbo.ApplicationUserHonoraryTitles");
            CreateIndex("dbo.HonoraryTitleApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.HonoraryTitleApplicationUsers", "HonoraryTitle_Id");
            AddForeignKey("dbo.HonoraryTitleApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HonoraryTitleApplicationUsers", "HonoraryTitle_Id", "dbo.HonoraryTitles", "Id", cascadeDelete: true);
            Sql(@"update Publications set PublicationType = PublicationType - 1 where PublicationType > 1;");
        }
    }
}
