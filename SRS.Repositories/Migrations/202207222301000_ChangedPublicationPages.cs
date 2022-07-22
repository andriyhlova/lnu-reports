namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPublicationPages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "PageFrom", c => c.Int());
            AddColumn("dbo.Publications", "PageTo", c => c.Int());
            Sql("update Publications set Pages=REPLACE(TRIM('-' from Pages),'--','-');");
            Sql(@"update Publications 
set PageFrom = SUBSTRING(Pages,1,CASE WHEN CHARINDEX('-', Pages) != 0 THEN CHARINDEX('-', Pages) - 1 ELSE LEN(Pages) END),
PageTo = SUBSTRING(Pages,CHARINDEX('-', Pages)+1,LEN(Pages));");
            DropColumn("dbo.Publications", "Pages");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Publications", "Pages", c => c.String());
            Sql(@"update Publications set Pages=(CASE WHEN PageFrom != PageTo THEN CONCAT(PageFrom,'-',PageTo) ELSE CAST(PageFrom as nvarchar(max)) END);");
            DropColumn("dbo.Publications", "PageTo");
            DropColumn("dbo.Publications", "PageFrom");
        }
    }
}
