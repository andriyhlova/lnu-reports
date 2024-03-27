namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDissertationDefenses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DissertationDefenses", "SpecializationId", c => c.Int());
            CreateIndex("dbo.DissertationDefenses", "SpecializationId");
            AddForeignKey("dbo.DissertationDefenses", "SpecializationId", "dbo.Specializations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DissertationDefenses", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.DissertationDefenses", new[] { "SpecializationId" });
            DropColumn("dbo.DissertationDefenses", "SpecializationId");
        }
    }
}
