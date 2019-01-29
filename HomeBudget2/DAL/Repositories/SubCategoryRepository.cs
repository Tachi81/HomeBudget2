using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;

namespace HomeBudget2.DAL.Repositories
{
    public class SubCategoryRepository : AbstractRepository<SubCategory>, ISubCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public SubCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}