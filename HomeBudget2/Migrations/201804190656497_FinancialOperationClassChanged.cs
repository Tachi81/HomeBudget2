namespace HomeBudget2.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FinancialOperationClassChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DestinationOfMoneys", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.DestinationOfMoneys", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.FinancialOperations", "DestinationOfMoneyId", "dbo.DestinationOfMoneys");
            DropForeignKey("dbo.SourceOMoneys", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.SourceOMoneys", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.FinancialOperations", "SourceOMoneyId", "dbo.SourceOMoneys");
            DropIndex("dbo.FinancialOperations", new[] { "SourceOMoneyId" });
            DropIndex("dbo.FinancialOperations", new[] { "DestinationOfMoneyId" });
            DropIndex("dbo.DestinationOfMoneys", new[] { "SubCategoryId" });
            DropIndex("dbo.DestinationOfMoneys", new[] { "BankAccountId" });
            DropIndex("dbo.SourceOMoneys", new[] { "SubCategoryId" });
            DropIndex("dbo.SourceOMoneys", new[] { "BankAccountId" });
            AddColumn("dbo.FinancialOperations", "BankAccountId", c => c.Int());
            AddColumn("dbo.FinancialOperations", "SubCategoryId", c => c.Int());
            AddColumn("dbo.FinancialOperations", "TargetBankAccountId", c => c.Int());
            AddColumn("dbo.FinancialOperations", "SourceOfMoney", c => c.String());
            AddColumn("dbo.FinancialOperations", "DestinationOfMoney", c => c.String());
            CreateIndex("dbo.FinancialOperations", "BankAccountId");
            CreateIndex("dbo.FinancialOperations", "SubCategoryId");
            CreateIndex("dbo.FinancialOperations", "TargetBankAccountId");
            AddForeignKey("dbo.FinancialOperations", "BankAccountId", "dbo.BankAccounts", "Id");
            AddForeignKey("dbo.FinancialOperations", "SubCategoryId", "dbo.SubCategories", "Id");
            AddForeignKey("dbo.FinancialOperations", "TargetBankAccountId", "dbo.BankAccounts", "Id");
            DropColumn("dbo.FinancialOperations", "DescriptionOfOperation");
            DropColumn("dbo.FinancialOperations", "SourceOMoneyId");
            DropColumn("dbo.FinancialOperations", "DestinationOfMoneyId");
            DropTable("dbo.DestinationOfMoneys");
            DropTable("dbo.SourceOMoneys");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.SourceOMoneys",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubCategoryId = c.Int(),
                    BankAccountId = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DestinationOfMoneys",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubCategoryId = c.Int(),
                    BankAccountId = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.FinancialOperations", "DestinationOfMoneyId", c => c.Int(nullable: false));
            AddColumn("dbo.FinancialOperations", "SourceOMoneyId", c => c.Int(nullable: false));
            AddColumn("dbo.FinancialOperations", "DescriptionOfOperation", c => c.String());
            DropForeignKey("dbo.FinancialOperations", "TargetBankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.FinancialOperations", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.FinancialOperations", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.FinancialOperations", new[] { "TargetBankAccountId" });
            DropIndex("dbo.FinancialOperations", new[] { "SubCategoryId" });
            DropIndex("dbo.FinancialOperations", new[] { "BankAccountId" });
            DropColumn("dbo.FinancialOperations", "DestinationOfMoney");
            DropColumn("dbo.FinancialOperations", "SourceOfMoney");
            DropColumn("dbo.FinancialOperations", "TargetBankAccountId");
            DropColumn("dbo.FinancialOperations", "SubCategoryId");
            DropColumn("dbo.FinancialOperations", "BankAccountId");
            CreateIndex("dbo.SourceOMoneys", "BankAccountId");
            CreateIndex("dbo.SourceOMoneys", "SubCategoryId");
            CreateIndex("dbo.DestinationOfMoneys", "BankAccountId");
            CreateIndex("dbo.DestinationOfMoneys", "SubCategoryId");
            CreateIndex("dbo.FinancialOperations", "DestinationOfMoneyId");
            CreateIndex("dbo.FinancialOperations", "SourceOMoneyId");
            AddForeignKey("dbo.FinancialOperations", "SourceOMoneyId", "dbo.SourceOMoneys", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SourceOMoneys", "SubCategoryId", "dbo.SubCategories", "Id");
            AddForeignKey("dbo.SourceOMoneys", "BankAccountId", "dbo.BankAccounts", "Id");
            AddForeignKey("dbo.FinancialOperations", "DestinationOfMoneyId", "dbo.DestinationOfMoneys", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DestinationOfMoneys", "SubCategoryId", "dbo.SubCategories", "Id");
            AddForeignKey("dbo.DestinationOfMoneys", "BankAccountId", "dbo.BankAccounts", "Id");
        }
    }
}
