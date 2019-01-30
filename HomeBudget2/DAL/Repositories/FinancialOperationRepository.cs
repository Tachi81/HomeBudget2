using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;

namespace HomeBudget2.DAL.Repositories
{
    public class FinancialOperationRepository : AbstractRepository<FinancialOperation>, IFinancialOperationRepository
    {
        private readonly ApplicationDbContext _context;
        public FinancialOperationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}