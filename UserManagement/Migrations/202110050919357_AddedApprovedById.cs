namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApprovedById : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ApprovedById", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "ApprovedById");
            AddForeignKey("dbo.AspNetUsers", "ApprovedById", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ApprovedById", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ApprovedById" });
            DropColumn("dbo.AspNetUsers", "ApprovedById");
        }
    }
}
