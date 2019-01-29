using HomeBudget2.BusinessLogic;
using HomeBudget2.DAL.Interfaces;
using HomeBudget2.DAL.Repositories;
using HomeBudget2.Models;

namespace HomeBudget2.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBankAccountRepository BankAccountRepo { get ; set ; }
        public ICategoryRepository CategoryRepo { get ; set ; }
        public IFinancialOperationRepository FinancialOperatiosRepo { get ; set ; }
        public ISubCategoryRepository SubCategoryRepo { get ; set ; }
        public IBankAccountLogic BankAccountLogic { get; set; }
        public IFinancialOperationService FinancialOperationService { get; set; }
        public ISubcategoryService subcategoryService { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            BankAccountRepo = new BankAccountRepository(context);
            CategoryRepo = new CategoryRepository(context);
            FinancialOperatiosRepo = new FinancialOperationRepository(context);
            SubCategoryRepo = new SubCategoryRepository(context);
            BankAccountLogic = new BankAccountLogic(this);

        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}