namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentRelatedTriggerCreated : DbMigration
    {
        public override void Up()
        {
            Sql(@"DROP TRIGGER IF EXISTS [dbo].[trgPaymentInsert]
                go
                Create trigger trgPaymentInsert on [dbo].[Payments]
                For Insert
                As

                Update SavingAccounts 
                Set LastPaymentDate = i.PaymentDate
                from inserted i
                where SavingAccounts.Id = i.SavingAccountId
                Go");

            Sql(@"DROP TRIGGER IF EXISTS [dbo].[trgPaymentDelete]
                go
                Create trigger trgPaymentDelete on [dbo].[Payments]
                For Delete
                As
                DECLARE @SavingAccountId int

                SET @SavingAccountId = (SELECT SavingAccountId FROM deleted)

                Update SavingAccounts 
                Set LastPaymentDate = 
                (select MAX(PaymentDate) PaymentDate from Payments where SavingAccountId = @SavingAccountId)
                where SavingAccounts.Id = @SavingAccountId

                Go");
        }
        
        public override void Down()
        {
            Sql("DROP TRIGGER IF EXISTS [dbo].[trgPaymentInsert]");
            Sql("DROP TRIGGER IF EXISTS [dbo].[trgPaymentDelete]");
        }
    }
}
