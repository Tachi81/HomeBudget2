namespace HomeBudget2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "UserId", c => c.String());
            AddColumn("dbo.FinancialOperations", "UserId", c => c.String());
            AddColumn("dbo.SubCategories", "UserId", c => c.String());
            AddColumn("dbo.Categories", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "UserId");
            DropColumn("dbo.SubCategories", "UserId");
            DropColumn("dbo.FinancialOperations", "UserId");
            DropColumn("dbo.BankAccounts", "UserId");
        }
    }
}
