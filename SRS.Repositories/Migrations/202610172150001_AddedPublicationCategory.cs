namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedPublicationCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "Category", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("dbo.Publications", "Category");
        }
    }
}
