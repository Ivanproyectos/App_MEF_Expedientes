using MEF.Expedientes.Data;
using MEF.Expedientes.Entity;
using MEF.Expedientes.Entity.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MEF.Expedientes.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext _context;
        private DbSet<T> _entities;

        public Repository(string connectionString = null)
        {
            _context = string.IsNullOrEmpty(connectionString)
                ? new DatabaseContext() : new DatabaseContext(connectionString);
        }

        public IEnumerable<T> GetAll() =>
        _context.Set<T>().ToList();

        public T Get(object key) =>
        _context.Set<T>().Find(key);

        public T Find(Expression<Func<T, bool>> match) =>
        _context.Set<T>().SingleOrDefault(match);

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match) =>
        _context.Set<T>().Where(match).ToList();

        public int FindAllCount(Expression<Func<T, bool>> match) =>
        _context.Set<T>().Where(match).ToList().Count;

        public IEnumerable<T> FindAllCustom(IQueryable<T> query)
        {
            //var result = new PagedResult<T>
            //{
            //    RowCount = query.Count()
            //};
            return query.ToList();
        }

        public IEnumerable<T> FindCustom(IQueryable<T> query)
        {
            //var result = new PagedResult<T>
            //{
            //    RowCount = query.Count()
            //};
            return query.ToList();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public long GetSequence(string sequenceName)
        {
            return _context.GetSequence(sequenceName);
        }

        public long GetQuery(string sqlQuery)
        {
            return _context.GetQuery(sqlQuery);
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public T Update(T entity, object key)
        {
            if (entity == null)
                return null;

            T existing = _context.Set<T>().Find(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
            return existing;
        }

        public T Update_Custom(T entity, object key, string[] campos)
        {
            if (entity == null)
                return null;

            T existing = _context.Set<T>().Find(key);
            //_context.Entry(existing).Attach(user);

            //  db.SaveChanges(); 
            if (existing != null)
            {
                IEnumerable<string> properties = _context.Entry(existing).GetDatabaseValues().PropertyNames;
                foreach (string campo in properties)
                {
                    var sadas = campos.FirstOrDefault(x => x.Equals(campo));
                    if (sadas == null)
                        _context.Entry(existing).Property(campo).IsModified = false;
                    else
                        _context.Entry(existing).Property(campo).IsModified = true;
                }

                //_context.Entry(existing).CurrentValues.SetValues(entity);
                //foreach (string campo in campos)
                //{
                //    _context.Entry(existing).Property(x => x.Equals(campo)).IsModified = true;
                //}


                //_context.Entry(existing).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
            return existing;
        }

        public void Update_Custom_All(IEnumerable<T> entities, string[] campos)
        {
            if (entities != null)
            {
                foreach (T existing in entities)
                {
                    IEnumerable<string> properties = _context.Entry(existing).GetDatabaseValues().PropertyNames;
                    foreach (string campo in properties)
                    {
                        var sadas = campos.FirstOrDefault(x => x.Equals(campo));
                        if (sadas == null)
                            _context.Entry(existing).Property(campo).IsModified = false;
                        else
                            _context.Entry(existing).Property(campo).IsModified = true;
                    }
                    //foreach (string campo in campos)
                    //{
                    //    _context.Entry(existing).Property(campo).IsModified = true;
                    //}
                }
                _context.SaveChanges();
            }
        }

 

        public void DeleteUno(long ID)
        {
            T existing = _context.Set<T>().Find(ID);
            _context.Entry(existing).State = EntityState.Deleted;
            // _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public int Count() =>
        _context.Set<T>().Count();

        public PagedResult<T> GetPaged(IQueryable<T> query, int page, int pageSize)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        #region Properties

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();

                return _entities;
            }
        }

        public void WriteToDatabase(string nombreTabla, DataTable dt, string[] col, ref Cls_Ent_Auditoria auditoria)
        {
            auditoria.Limpiar();
            try
            {
                _context.WriteToDatabase(nombreTabla, dt, col, ref auditoria);
            }
            catch (Exception ex)
            {
                auditoria.Error(ex);
            }
        }
        #endregion
    }
}
