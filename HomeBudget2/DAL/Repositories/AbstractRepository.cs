using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HomeBudget2.DAL.Repositories
{
    public class AbstractRepository<T> : IAbstractRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public AbstractRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual void Create(T entity)
        {
            using (_context)
            {
                _context.Set<T>().Add(entity);                
            }
        }

        public virtual void Delete(T entity)
        {
           using (_context)
            {
                _context.Entry(entity).State = EntityState.Deleted;                
            }
        }

        public virtual List<T> GetWhere(Expression<Func<T, bool>> expression)
        {
           using (_context)
            {
                var query = _context.Set<T>().Where(expression);
                return query.ToList();
            }
        }

        public virtual List<T> GetWhereWithIncludes(Expression<Func<T, bool>> expressionWhere, params Expression<Func<T, object>>[] includes)
        {
           using (_context)
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (Expression<Func<T, object>> include in includes)
                    query = query.Include(include);
                query = query.Where(expressionWhere);
                return query.ToList();
            }
        }

        public virtual void Update(T entity)
        {
           using (_context)
            {
                _context.Entry(entity).State = EntityState.Modified;
                
            }
        }
    }
}