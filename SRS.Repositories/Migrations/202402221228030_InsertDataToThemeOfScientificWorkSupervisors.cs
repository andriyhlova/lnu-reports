namespace SRS.Repositories.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    
    public partial class InsertDataToThemeOfScientificWorkSupervisors : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[ThemeOfScientificWorkSupervisors] (ThemeOfScientificWorkId, SupervisorId)\r\n" +
                "SELECT Id ,SupervisorId\r\n" +
                "FROM [dbo].[ThemeOfScientificWorks]\r\n" +
                "where SupervisorId != 'NULL';");
        }
        
        public override void Down()
        {
            Sql("UPDATE tw\r\n" +
                "SET tw.SupervisorId = tws.SupervisorId\r\n" +
                "FROM dbo.ThemeOfScientificWorks tw\r\n" +
                "INNER JOIN dbo.ThemeOfScientificWorkSupervisors tws\r\n" +
                "ON tw.ID = tws.ThemeOfScientificWorkId;");
        }
    }
}
