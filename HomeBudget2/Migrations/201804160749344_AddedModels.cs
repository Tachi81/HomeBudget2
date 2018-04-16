namespace HomeBudget2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InitialBalance = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        AccountName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FinancialOperations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AmountOfMoney = c.Double(nullable: false),
                        DescriptionOfOperation = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Note = c.String(),
                        SourceOMoneyId = c.Int(nullable: false),
                        DestinationOfMoneyId = c.Int(nullable: false),
                        IsTransfer = c.Boolean(nullable: false),
                        IsExpense = c.Boolean(nullable: false),
                        IsIncome = c.Boolean(nullable: false),
                        BankAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DestinationOfMoneys", t => t.DestinationOfMoneyId, cascadeDelete: true)
                .ForeignKey("dbo.SourceOMoneys", t => t.SourceOMoneyId, cascadeDelete: true)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_Id)
                .Index(t => t.SourceOMoneyId)
                .Index(t => t.DestinationOfMoneyId)
                .Index(t => t.BankAccount_Id);
            
            CreateTable(
                "dbo.DestinationOfMoneys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(),
                        BankAccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        IsExpense = c.Boolean(nullable: false),
                        IsIncome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        IsExpense = c.Boolean(nullable: false),
                        IsIncome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SourceOMoneys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(),
                        BankAccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.BankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FinancialOperations", "BankAccount_Id", "dbo.BankAccounts");
            DropForeignKey("dbo.FinancialOperations", "SourceOMoneyId", "dbo.SourceOMoneys");
            DropForeignKey("dbo.SourceOMoneys", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.SourceOMoneys", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.FinancialOperations", "DestinationOfMoneyId", "dbo.DestinationOfMoneys");
            DropForeignKey("dbo.DestinationOfMoneys", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.DestinationOfMoneys", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.SourceOMoneys", new[] { "BankAccountId" });
            DropIndex("dbo.SourceOMoneys", new[] { "SubCategoryId" });
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropIndex("dbo.DestinationOfMoneys", new[] { "BankAccountId" });
            DropIndex("dbo.DestinationOfMoneys", new[] { "SubCategoryId" });
            DropIndex("dbo.FinancialOperations", new[] { "BankAccount_Id" });
            DropIndex("dbo.FinancialOperations", new[] { "DestinationOfMoneyId" });
            DropIndex("dbo.FinancialOperations", new[] { "SourceOMoneyId" });
            DropTable("dbo.SourceOMoneys");
            DropTable("dbo.Categories");
            DropTable("dbo.SubCategories");
            DropTable("dbo.DestinationOfMoneys");
            DropTable("dbo.FinancialOperations");
            DropTable("dbo.BankAccounts");
        }
    }
}
