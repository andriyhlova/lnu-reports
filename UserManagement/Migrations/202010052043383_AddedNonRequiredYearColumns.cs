namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNonRequiredYearColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "GraduationDate", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "AwardingDate", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "DefenseYear", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DefenseYear", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "AwardingDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "GraduationDate", c => c.DateTime(nullable: false));
        }
    }
}
