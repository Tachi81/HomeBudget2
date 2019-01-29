using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;

namespace HomeBudget2.DAL.Repositories
{
    public class BankAccountRepository : AbstractRepository<BankAccount>, IBankAccountRepository
    {
        private readonly ApplicationDbContext _context;
       public BankAccountRepository (ApplicationDbContext context)
        {
            _context = context;
        }
    }
}