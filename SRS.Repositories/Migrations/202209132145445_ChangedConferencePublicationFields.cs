namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedConferencePublicationFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "ConferencePlace", c => c.String());
            AlterColumn("dbo.Publications", "ConferenceDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publications", "ConferenceDate", c => c.DateTime());
            DropColumn("dbo.Publications", "ConferencePlace");
        }
    }
}
