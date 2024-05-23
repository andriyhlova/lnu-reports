namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDissertationDefenses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DissertationDefenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Theme = c.String(),
                        DefenseDate = c.DateTime(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
                        SupervisorId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        DissertationType = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SupervisorId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.SupervisorId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DissertationDefenses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DissertationDefenses", "SupervisorId", "dbo.AspNetUsers");
            DropIndex("dbo.DissertationDefenses", new[] { "UserId" });
            DropIndex("dbo.DissertationDefenses", new[] { "SupervisorId" });
            DropTable("dbo.DissertationDefenses");
        }
    }
}
