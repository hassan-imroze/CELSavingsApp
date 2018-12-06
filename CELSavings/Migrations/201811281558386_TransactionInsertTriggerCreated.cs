namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionInsertTriggerCreated : DbMigration
    {
        public override void Up()
        {
            Sql(@"DROP TRIGGER IF EXISTS [dbo].[trgTransactionInsert]
                go
                Create trigger trgTransactionInsert on [dbo].[Transactions]
                For Insert
                As

                Update SavingAccounts
                Set Balance = Balance + i.Amount,
                LastTransactionDate = i.TransactionDate
                from 
                (select SavingAccountId,
                case TransactionSide
                when 1 Then Amount
                else -1 * Amount 
                END Amount,TransactionDate from inserted) i
                where SavingAccounts.Id = i.SavingAccountId
                Go");
        }
        
        public override void Down()
        {
            Sql(@"DROP TRIGGER IF EXISTS [dbo].[trgTransactionInsert]");
        }
    }
}
