using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HomeBudget2.DAL.Interfaces
{
    public interface IAbstractRepository<T> where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        List<T> GetWhere(Expression<Func<T, bool>> expression);
        void Update(T entity);
        List<T> GetWhereWithIncludes(Expression<Func<T, bool>> expressionWhere,
            params Expression<Func<T, object>>[] includes);
    }
}