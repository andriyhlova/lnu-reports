namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RenamedScienceDegreeAcademicStatusTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AcademicStatus", newName: "Degrees");
            RenameColumn(table: "dbo.AspNetUsers", name: "AcademicStatus_Id", newName: "Degree_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_AcademicStatus_Id", newName: "IX_Degree_Id");
            RenameTable(name: "dbo.ScienceDegrees", newName: "AcademicStatus");
            RenameColumn(table: "dbo.AspNetUsers", name: "ScienceDegree_Id", newName: "AcademicStatus_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_ScienceDegree_Id", newName: "IX_AcademicStatus_Id");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_AcademicStatus_Id", newName: "IX_ScienceDegree_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "AcademicStatus_Id", newName: "ScienceDegree_Id");
            RenameTable(name: "dbo.AcademicStatus", newName: "ScienceDegrees");
            RenameTable(name: "dbo.Degrees", newName: "AcademicStatus");
            RenameColumn(table: "dbo.AspNetUsers", name: "Degrees_Id", newName: "AcademicStatus_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Degrees_Id", newName: "IX_AcademicStatus_Id");
        }
    }
}
