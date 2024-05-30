namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSpecializations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DissertationDefenses", "YearOfGraduating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DissertationDefenses", "YearOfGraduating");
            DropTable("dbo.Specializations");
        }
    }
}
