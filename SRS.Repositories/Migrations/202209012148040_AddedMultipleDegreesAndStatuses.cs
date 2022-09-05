namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMultipleDegreesAndStatuses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "AcademicStatus_Id", "dbo.AcademicStatus");
            DropForeignKey("dbo.AspNetUsers", "Degree_Id", "dbo.Degrees");
            Sql(@"IF (OBJECT_ID('[FK_dbo.AspNetUsers_dbo.ScienceDegrees_ScienceDegree_ID]', 'F') IS NOT NULL)
            BEGIN
                ALTER TABLE dbo.AspNetUsers DROP CONSTRAINT ""FK_dbo.AspNetUsers_dbo.ScienceDegrees_ScienceDegree_ID""
            END;");
            DropIndex("dbo.AspNetUsers", new[] { "Degree_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "AcademicStatus_Id" });
            CreateTable(
                "dbo.ApplicationUserAcademicStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AwardDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        AcademicStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicStatus", t => t.AcademicStatusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AcademicStatusId);
            Sql(@"insert into dbo.ApplicationUserAcademicStatus(AcademicStatusId, UserId, AwardDate) 
                  select AcademicStatus_Id, Id, AwardingDate from dbo.AspNetUsers where AcademicStatus_Id is not null and AwardingDate is not null;");

            CreateTable(
                "dbo.ApplicationUserDegrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AwardDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DegreeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Degrees", t => t.DegreeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.DegreeId);
            Sql(@"insert into dbo.ApplicationUserDegrees(DegreeId, UserId, AwardDate) 
                  select Degree_Id, Id, DefenseYear from dbo.AspNetUsers where Degree_Id is not null and DefenseYear is not null;");

            CreateTable(
                "dbo.HonoraryTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql(@"insert into dbo.HonoraryTitles(Value) values
                  (N'Лауреат Державної премії України в галузі науки і техніки'),
                  (N'Заслужений діяч науки і техніки України'),
                  (N'Заслужений професор Львівського університету'),
                  (N'Лауреат Премії Верховної Ради України найталановитішим молодим ученим у галузі фундаментальних і прикладних досліджень та науково-технічних розробок'),
                  (N'Лауреат Премії Президента України для молодих учених')");

            CreateTable(
                "dbo.HonoraryTitleApplicationUsers",
                c => new
                    {
                        HonoraryTitle_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.HonoraryTitle_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.HonoraryTitles", t => t.HonoraryTitle_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.HonoraryTitle_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "DegreeDefenseYear", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "AcademicStatusDefenseYear", c => c.DateTime());
            Sql(@"update dbo.AspNetUsers 
set DegreeDefenseYear = (case when DoctorFinishYear is null and AspirantFinishYear is not null then DefenseYear else null end),
AcademicStatusDefenseYear = (case when DoctorFinishYear is not null then DefenseYear else null end);");
            DropColumn("dbo.AspNetUsers", "DefenseYear");
            DropColumn("dbo.AspNetUsers", "Degree_Id");
            DropColumn("dbo.AspNetUsers", "AcademicStatus_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AcademicStatus_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Degree_Id", c => c.Int());
            Sql(@"update dbo.AspNetUsers set dbo.AspNetUsers.Degree_Id = ud.DegreeId
                    from
                        dbo.AspNetUsers u
                    inner join
                        dbo.ApplicationUserDegrees ud
                    on 
                        u.ID = ud.UserId;");
            Sql(@"update dbo.AspNetUsers set dbo.AspNetUsers.AcademicStatus_Id = us.AcademicStatusId
                    from
                        dbo.AspNetUsers u
                    inner join
                        dbo.ApplicationUserAcademicStatus us
                    on 
                        u.ID = us.UserId;");
            AddColumn("dbo.AspNetUsers", "DefenseYear", c => c.DateTime());
            Sql(@"update dbo.AspNetUsers 
set DefenseYear = (case when AcademicStatusDefenseYear is not null then AcademicStatusDefenseYear when DegreeDefenseYear is not null then DegreeDefenseYear else null end);");
            DropForeignKey("dbo.HonoraryTitleApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HonoraryTitleApplicationUsers", "HonoraryTitle_Id", "dbo.HonoraryTitles");
            DropForeignKey("dbo.ApplicationUserDegrees", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserDegrees", "DegreeId", "dbo.Degrees");
            DropForeignKey("dbo.ApplicationUserAcademicStatus", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserAcademicStatus", "AcademicStatusId", "dbo.AcademicStatus");
            DropIndex("dbo.HonoraryTitleApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.HonoraryTitleApplicationUsers", new[] { "HonoraryTitle_Id" });
            DropIndex("dbo.ApplicationUserDegrees", new[] { "DegreeId" });
            DropIndex("dbo.ApplicationUserDegrees", new[] { "UserId" });
            DropIndex("dbo.ApplicationUserAcademicStatus", new[] { "AcademicStatusId" });
            DropIndex("dbo.ApplicationUserAcademicStatus", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "AcademicStatusDefenseYear");
            DropColumn("dbo.AspNetUsers", "DegreeDefenseYear");
            DropTable("dbo.HonoraryTitleApplicationUsers");
            DropTable("dbo.HonoraryTitles");
            DropTable("dbo.ApplicationUserDegrees");
            DropTable("dbo.ApplicationUserAcademicStatus");
            CreateIndex("dbo.AspNetUsers", "AcademicStatus_Id");
            CreateIndex("dbo.AspNetUsers", "Degree_Id");
            AddForeignKey("dbo.AspNetUsers", "Degree_Id", "dbo.Degrees", "Id");
            AddForeignKey("dbo.AspNetUsers", "AcademicStatus_Id", "dbo.AcademicStatus", "Id");
        }
    }
}
