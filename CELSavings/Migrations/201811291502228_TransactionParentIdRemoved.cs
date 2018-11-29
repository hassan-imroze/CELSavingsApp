namespace CELSavings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionParentIdRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transactions", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "ParentId", c => c.Int(nullable: false));
        }
    }
}
