namespace HomeBudget2.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IBankAccountRepository BankAccountRepo { get; set; }
        ICategoryRepository CategoryRepo { get; set; }
        IFinancialOperationRepository FinancialOperatiosRepo { get; set; }
        ISubCategoryRepository SubCategoryRepo { get; set; }
        IBankAccountLogic BankAccountLogic { get; set; }
        IFinancialOperationService FinancialOperationService { get; set; }
        ISubcategoryService SubcategoryService { get; set; }

        void Complete();


    }
}
