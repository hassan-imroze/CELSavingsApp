namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SavingAccountChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavingAccounts", "AccountNo", c => c.String(nullable: false, maxLength: 6));
            AddColumn("dbo.SavingAccounts", "Email", c => c.String());
            AddColumn("dbo.SavingAccounts", "LastTransactionDate", c => c.DateTime());
            AlterColumn("dbo.SavingAccounts", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.SavingAccounts", "AccountNo", unique: true);
            CreateIndex("dbo.SavingAccounts", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.SavingAccounts", new[] { "Name" });
            DropIndex("dbo.SavingAccounts", new[] { "AccountNo" });
            AlterColumn("dbo.SavingAccounts", "Name", c => c.String());
            DropColumn("dbo.SavingAccounts", "LastTransactionDate");
            DropColumn("dbo.SavingAccounts", "Email");
            DropColumn("dbo.SavingAccounts", "AccountNo");
        }
    }
}
