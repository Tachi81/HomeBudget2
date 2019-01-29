namespace HomeBudget2.DAL.Interfaces
{
    public interface IUnitOfWork
    {
         IBankAccountRepository BankAccountRepo{ get; set; }
         ICategoryRepository CategoryRepo { get; set; }
        IFinancialOperationRepository FinancialOperatiosRepo { get; set; }
        ISubCategoryRepository subCategoryRepo { get; set; }

        void Complete();


    }
}
