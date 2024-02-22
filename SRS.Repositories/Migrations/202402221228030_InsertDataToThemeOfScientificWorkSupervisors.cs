namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertDataToThemeOfScientificWorkSupervisors : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [AttendanceDB].[dbo].[ThemeOfScientificWorkSupervisors] (ThemeOfScientificWorkId, SupervisorId)\r\n" +
                "SELECT Id ,SupervisorId\r\n" +
                "FROM [AttendanceDB].[dbo].[ThemeOfScientificWorks]\r\n" +
                "where SupervisorId != 'NULL';");
        }
        
        public override void Down()
        {
            Sql("WITH MinIdPerTheme AS (\r\n " +
                "SELECT ThemeOfScientificWorkId, MIN(Id) AS MinId\r\n  " +
                "FROM [AttendanceDB].[dbo].[ThemeOfScientificWorkSupervisors]\r\n  " +
                "GROUP BY ThemeOfScientificWorkId\r\n" +
                ")\r\n" +
                "\r\n" +
                "UPDATE tw" +
                "\r\n" +
                "SET tw.SupervisorId = sw.SupervisorId\r\n" +
                "FROM [AttendanceDB].[dbo].[ThemeOfScientificWorks] tw\r\n" +
                "INNER JOIN (SELECT t.ThemeOfScientificWorkId, t.SupervisorId\r\n" +
                "FROM [AttendanceDB].[dbo].[ThemeOfScientificWorkSupervisors] t\r\n" +
                "INNER JOIN MinIdPerTheme m ON t.ThemeOfScientificWorkId = m.ThemeOfScientificWorkId AND t.Id = m.MinId) sw\r\n" +
                "ON tw.Id = sw.ThemeOfScientificWorkId;");

        }
    }
}
