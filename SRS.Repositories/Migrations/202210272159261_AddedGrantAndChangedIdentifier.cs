namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGrantAndChangedIdentifier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "OtherGrantDescription", c => c.String());
            AlterColumn("dbo.Publications", "PublicationIdentifier", c => c.String());
        }
        
        public override void Down()
        {
            Sql(@"update Publications set PublicationIdentifier=TRY_CAST(PublicationIdentifier as int) where PublicationIdentifier is not null;");
            AlterColumn("dbo.Publications", "PublicationIdentifier", c => c.Int());
            DropColumn("dbo.Reports", "OtherGrantDescription");
        }
    }
}
