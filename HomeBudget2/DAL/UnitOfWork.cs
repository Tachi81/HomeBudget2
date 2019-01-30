using HomeBudget2.BusinessLogic;
using HomeBudget2.DAL.Interfaces;
using HomeBudget2.DAL.Repositories;
using HomeBudget2.Models;
using HomeBudget2.Service;

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
        public ISubcategoryService SubcategoryService { get; set; }
         

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            BankAccountRepo = new BankAccountRepository(context);
            CategoryRepo = new CategoryRepository(context);
            FinancialOperatiosRepo = new FinancialOperationRepository(context);
            SubCategoryRepo = new SubCategoryRepository(context);
            BankAccountLogic = new BankAccountLogic(this);
            FinancialOperationService = new FinancialOperationService(this);
            SubcategoryService = new SubcategoryService(this);
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}