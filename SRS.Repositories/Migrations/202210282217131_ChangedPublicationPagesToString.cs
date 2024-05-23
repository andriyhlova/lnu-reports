namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPublicationPagesToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publications", "PageFrom", c => c.String());
            AlterColumn("dbo.Publications", "PageTo", c => c.String());
        }
        
        public override void Down()
        {
            Sql(@"update Publications set PageTo=TRY_CAST(PageTo as int) where PageTo is not null;");
            Sql(@"update Publications set PageFrom=TRY_CAST(PageFrom as int) where PageFrom is not null;");
            AlterColumn("dbo.Publications", "PageTo", c => c.Int());
            AlterColumn("dbo.Publications", "PageFrom", c => c.Int());
        }
    }
}
