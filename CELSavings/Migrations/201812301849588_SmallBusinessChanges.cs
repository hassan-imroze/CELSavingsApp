namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallBusinessChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmallBusinessSavingAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmallBusinessId = c.Int(nullable: false),
                        SavingAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SavingAccounts", t => t.SavingAccountId, cascadeDelete: false)
                .ForeignKey("dbo.SmallBusinesses", t => t.SmallBusinessId, cascadeDelete: true)
                .Index(t => t.SmallBusinessId)
                .Index(t => t.SavingAccountId);
            
            AddColumn("dbo.SmallBusinesses", "CustomerPhone", c => c.String());
            AddColumn("dbo.SmallBusinesses", "ProfitPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SmallBusinesses", "InstallmentStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SmallBusinesses", "PaymentDueDate", c => c.DateTime());
            DropColumn("dbo.SmallBusinesses", "CustomerMobile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SmallBusinesses", "CustomerMobile", c => c.String());
            DropForeignKey("dbo.SmallBusinessSavingAccounts", "SmallBusinessId", "dbo.SmallBusinesses");
            DropForeignKey("dbo.SmallBusinessSavingAccounts", "SavingAccountId", "dbo.SavingAccounts");
            DropIndex("dbo.SmallBusinessSavingAccounts", new[] { "SavingAccountId" });
            DropIndex("dbo.SmallBusinessSavingAccounts", new[] { "SmallBusinessId" });
            DropColumn("dbo.SmallBusinesses", "PaymentDueDate");
            DropColumn("dbo.SmallBusinesses", "InstallmentStartDate");
            DropColumn("dbo.SmallBusinesses", "ProfitPercentage");
            DropColumn("dbo.SmallBusinesses", "CustomerPhone");
            DropTable("dbo.SmallBusinessSavingAccounts");
        }
    }
}
