using MEF.Expedientes.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MEF.Expedientes.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(object key);
        T Find(Expression<Func<T, bool>> match);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match);
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        void Update(T entity);
        T Update(T entity, object key);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        int Count();
        PagedResult<T> GetPaged(IQueryable<T> query, int page, int pageSize);
    }
}
