namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentAndTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SavingAccountId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentMonth = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 12, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SavingAccountId = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        ParentId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        TransactionSide = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 12, scale: 4),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SavingAccounts", "Mobile", c => c.String());
            AddColumn("dbo.SavingAccounts", "NID", c => c.String());
            AddColumn("dbo.SavingAccounts", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavingAccounts", "Status");
            DropColumn("dbo.SavingAccounts", "NID");
            DropColumn("dbo.SavingAccounts", "Mobile");
            DropTable("dbo.Transactions");
            DropTable("dbo.Payments");
        }
    }
}
