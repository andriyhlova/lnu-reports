namespace SRS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserFieldsToReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "UserFullName", c => c.String());
            AddColumn("dbo.Reports", "PositionName", c => c.String());
            AddColumn("dbo.Reports", "CathedraName", c => c.String());
            AddColumn("dbo.Reports", "CathedraLeadName", c => c.String());
            AddColumn("dbo.Reports", "ScopusHIndex", c => c.Int());
            AddColumn("dbo.Reports", "WebOfScienceHIndex", c => c.Int());
            AddColumn("dbo.Reports", "GoogleScholarHIndex", c => c.Int());
            Sql(@"update reports
set UserFullName=i.LastName + ' ' + i.FirstName + ' ' + i.FathersName,
PositionName = p.Value,
CathedraName = c.GenitiveCase,
CathedraLeadName =  (case when ic.FirstName is not null then left(ic.FirstName, 1)+'.' else '' end) + ' ' + (case when ic.FathersName is not null then left(ic.FathersName, 1)+'.' else '' end) + ' ' + ic.LastName,
GoogleScholarHIndex = u.GoogleScholarHIndex,
ScopusHIndex = u.ScopusHIndex,
WebOfScienceHIndex = u.WebOfScienceHIndex
from reports r 
join AspNetUsers u on r.User_Id=u.Id
left join I18nUserInitials i on i.User_Id=u.Id and i.Language=0
left join Cathedras c on u.Cathedra_ID=c.ID
left join Positions p on u.Position_ID = p.ID
left join (select min(Id) as Id, Cathedra_ID from AspNetUsers where Position_ID=2 group by Cathedra_ID) uc on uc.Cathedra_ID=c.ID
left join I18nUserInitials ic on ic.User_Id=uc.Id and ic.Language=0
where State!=0;");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "GoogleScholarHIndex");
            DropColumn("dbo.Reports", "WebOfScienceHIndex");
            DropColumn("dbo.Reports", "ScopusHIndex");
            DropColumn("dbo.Reports", "CathedraLeadName");
            DropColumn("dbo.Reports", "CathedraName");
            DropColumn("dbo.Reports", "PositionName");
            DropColumn("dbo.Reports", "UserFullName");
        }
    }
}
