namespace HomeBudget2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinancialOperationClassChanged1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinancialOperations", "SourceOfMoney", c => c.String());
            DropColumn("dbo.FinancialOperations", "SourceOMoney");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FinancialOperations", "SourceOMoney", c => c.String());
            DropColumn("dbo.FinancialOperations", "SourceOfMoney");
        }
    }
}
