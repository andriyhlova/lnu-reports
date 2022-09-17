namespace SRS.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUserRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE AspNetUserRoles
                DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId];");

            Sql(@"update AspNetUserRoles
                set RoleId = 
                (case when AspNetRoles.Name=N'Superadmin' then '1' 
                when AspNetRoles.Name=N'Адміністрація ректорату' then '2'
                when AspNetRoles.Name=N'Адміністрація деканату' then '3'
                when AspNetRoles.Name=N'Керівник кафедри' then '4' 
                when AspNetRoles.Name=N'Працівник' then '5'
                else RoleId end)
                FROM AspNetUserRoles JOIN AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id;");

            Sql(@"delete from AspNetRoles;");

            Sql(@"Insert into AspNetRoles(Id, Name) values
                ('1', N'Superadmin'),
                ('2', N'Адміністрація ректорату'),
                ('3', N'Адміністрація деканату'),
                ('4', N'Керівник кафедри'),
                ('5', N'Працівник');");

            Sql(@"ALTER TABLE AspNetUserRoles
                ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
                FOREIGN KEY(RoleId) REFERENCES AspNetRoles(Id);");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S1186:Methods should not be empty", Justification = "No need to revert")]
        public override void Down()
        {
        }
    }
}
