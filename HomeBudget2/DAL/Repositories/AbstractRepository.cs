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
        public virtual void Create(T entity)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public virtual void Delete(T entity)
        {
            using (var context = new ApplicationDbContext())
            {

                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual List<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            using (var context = new ApplicationDbContext())
            {
                var query = context.Set<T>().Where(expression);
                return query.ToList();
            }
        }

        public virtual List<T> GetWhereWithIncludes(Expression<Func<T, bool>> expressionWhere, params Expression<Func<T, object>>[] includes)
        {
            using (var context = new ApplicationDbContext())
            {
                IQueryable<T> query = context.Set<T>();
                foreach (Expression<Func<T, object>> include in includes)
                    query = query.Include(include);
                query = query.Where(expressionWhere);
                return query.ToList();
            }
        }

        public virtual void Update(T entity)
        {
            using (var context = new ApplicationDbContext())
            {

                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}