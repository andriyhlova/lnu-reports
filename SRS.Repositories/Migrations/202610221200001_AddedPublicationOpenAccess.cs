namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedPublicationOpenAccess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "IsOpenAccess", c => c.Boolean());
        }

        public override void Down()
        {
            DropColumn("dbo.Publications", "IsOpenAccess");
        }
    }
}
