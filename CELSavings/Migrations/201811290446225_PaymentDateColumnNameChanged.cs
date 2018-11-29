namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentDateColumnNameChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PaymentMonthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SavingAccounts", "LastPaymentMonthDate", c => c.DateTime());
            DropColumn("dbo.Payments", "PaymentDate");
            DropColumn("dbo.SavingAccounts", "LastPaymentDate");

            Sql(@"DROP TRIGGER IF EXISTS [dbo].[trgPaymentInsert]
                go
                Create trigger trgPaymentInsert on [dbo].[Payments]
                For Insert
                As

                Update SavingAccounts 
                Set LastPaymentMonthDate = i.PaymentMonthDate
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
                Set LastPaymentMonthDate = 
                (select MAX(PaymentMonthDate) PaymentMonthDate from Payments where SavingAccountId = @SavingAccountId)
                where SavingAccounts.Id = @SavingAccountId

                Go");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavingAccounts", "LastPaymentDate", c => c.DateTime());
            AddColumn("dbo.Payments", "PaymentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.SavingAccounts", "LastPaymentMonthDate");
            DropColumn("dbo.Payments", "PaymentMonthDate");
        }
    }
}
