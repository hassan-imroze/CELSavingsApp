namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers]
                ([Id],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName])
                 VALUES('be55a905-e9a9-4f53-aa28-7676c8d88e12','tanvir@celimited.com',1,'ANxX1dwJjP+8rTy8dntY0c4cYJU4Xub6leu/h2hEe6StZZhPYRcNNcBDJ+RkV1FSfg==','05cd8bf3-13a6-4a90-b680-0275bef44f1b',NULL,0,0,NULL,1,0,'tanvir@celimited.com')
                GO
                INSERT INTO [dbo].[AspNetRoles] ([Id],[Name]) VALUES ('ea159d53-7827-4317-a6cc-5be0888e66f1','CanManageSavingAccounts')
                GO
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId]) VALUES ('be55a905-e9a9-4f53-aa28-7676c8d88e12' ,'ea159d53-7827-4317-a6cc-5be0888e66f1')
                GO");
        }
        
        public override void Down()
        {

        }
    }
}
