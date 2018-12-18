namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createrelationBetweenPaentandSavingAccount : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Payments", "SavingAccountId");
            AddForeignKey("dbo.Payments", "SavingAccountId", "dbo.SavingAccounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "SavingAccountId", "dbo.SavingAccounts");
            DropIndex("dbo.Payments", new[] { "SavingAccountId" });
        }
    }
}
