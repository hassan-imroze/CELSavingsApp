namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionRelatedUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "PaymentId", c => c.Int());
            CreateIndex("dbo.Transactions", "SavingAccountId");
            CreateIndex("dbo.Transactions", "PaymentId");
            AddForeignKey("dbo.Transactions", "PaymentId", "dbo.Payments", "Id");
            AddForeignKey("dbo.Transactions", "SavingAccountId", "dbo.SavingAccounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "SavingAccountId", "dbo.SavingAccounts");
            DropForeignKey("dbo.Transactions", "PaymentId", "dbo.Payments");
            DropIndex("dbo.Transactions", new[] { "PaymentId" });
            DropIndex("dbo.Transactions", new[] { "SavingAccountId" });
            DropColumn("dbo.Transactions", "PaymentId");
        }
    }
}
