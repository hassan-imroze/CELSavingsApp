namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSmallBusinessObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmallBusinesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product = c.String(nullable: false),
                        ProductDescription = c.String(),
                        BuyingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerName = c.String(nullable: false),
                        CustomerMobile = c.String(),
                        CustomerOrGuarantorId = c.Int(nullable: false),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellDate = c.DateTime(nullable: false),
                        InitialPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentReceived = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SavingAccounts", t => t.CustomerOrGuarantorId, cascadeDelete: true)
                .Index(t => t.CustomerOrGuarantorId);
            
            CreateTable(
                "dbo.SmallBusinessInstallments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmallBusinessId = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SmallBusinesses", t => t.SmallBusinessId, cascadeDelete: true)
                .Index(t => t.SmallBusinessId);
            
            CreateTable(
                "dbo.SmallBusinessPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmallBusinessId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SmallBusinesses", t => t.SmallBusinessId, cascadeDelete: true)
                .Index(t => t.SmallBusinessId);
            
            AddColumn("dbo.Transactions", "SmallBusinessId", c => c.Int());
            AddColumn("dbo.Transactions", "SmallBusinessPaymentId", c => c.Int());
            CreateIndex("dbo.Transactions", "SmallBusinessId");
            CreateIndex("dbo.Transactions", "SmallBusinessPaymentId");
            AddForeignKey("dbo.Transactions", "SmallBusinessPaymentId", "dbo.SmallBusinessPayments", "Id");
            AddForeignKey("dbo.Transactions", "SmallBusinessId", "dbo.SmallBusinesses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "SmallBusinessId", "dbo.SmallBusinesses");
            DropForeignKey("dbo.Transactions", "SmallBusinessPaymentId", "dbo.SmallBusinessPayments");
            DropForeignKey("dbo.SmallBusinessPayments", "SmallBusinessId", "dbo.SmallBusinesses");
            DropForeignKey("dbo.SmallBusinessInstallments", "SmallBusinessId", "dbo.SmallBusinesses");
            DropForeignKey("dbo.SmallBusinesses", "CustomerOrGuarantorId", "dbo.SavingAccounts");
            DropIndex("dbo.SmallBusinessPayments", new[] { "SmallBusinessId" });
            DropIndex("dbo.SmallBusinessInstallments", new[] { "SmallBusinessId" });
            DropIndex("dbo.SmallBusinesses", new[] { "CustomerOrGuarantorId" });
            DropIndex("dbo.Transactions", new[] { "SmallBusinessPaymentId" });
            DropIndex("dbo.Transactions", new[] { "SmallBusinessId" });
            DropColumn("dbo.Transactions", "SmallBusinessPaymentId");
            DropColumn("dbo.Transactions", "SmallBusinessId");
            DropTable("dbo.SmallBusinessPayments");
            DropTable("dbo.SmallBusinessInstallments");
            DropTable("dbo.SmallBusinesses");
        }
    }
}
