using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;

namespace HomeBudget2.DAL.Repositories
{
    public class CategoryRepository : AbstractRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
       public CategoryRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }
    }
}