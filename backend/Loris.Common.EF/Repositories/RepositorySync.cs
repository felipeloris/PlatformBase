using Loris.Common.Domain.Interfaces;
using Loris.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Loris.Common.EF.Repository
{
    public abstract class RepositorySync<TEntityId, TContext> : 
        RepositoryBase<TEntityId, TContext>, IRepository<TEntityId>
        where TEntityId : class, IEntityIdBase
        where TContext : DbContext
    {
        public RepositorySync() : base() { }

        public RepositorySync(IDatabase database) : base(database) { }

        public RepositorySync(TContext context) : base(context) { }

        public TEntityId Add(TEntityId obj)
        {
            try
            {
                DbSet.Add(obj);
                dbContext.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Insert", ref ex))
                    throw ex;
                throw;
            }
        }

        public bool Update(TEntityId obj)
        {
            try
            {
                dbContext.Entry<TEntityId>(obj).State = EntityState.Modified;
                return (dbContext.SaveChanges() > 0);
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Update", ref ex))
                    throw ex;
                throw;
            }
        }

        public bool Delete(TEntityId obj)
        {
            try
            {
                dbContext.Entry(obj).State = EntityState.Deleted;
                return (dbContext.SaveChanges() > 0);
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Delete", ref ex))
                    throw ex;
                throw;
            }
        }

        public bool Delete(Expression<Func<TEntityId, bool>> where)
        {
            try
            {
                TEntityId obj = DbSet.Where(where).FirstOrDefault();
                return (obj != null) 
                    ? Delete(obj) 
                    : false;
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Delete by expression", ref ex))
                    throw ex;
                throw;
            }
        }

        public bool Delete(params object[] keys)
        {
            try
            {
                TEntityId obj = DbSet.Find(keys);
                return (obj != null)
                    ? Delete(obj)
                    : false;
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Delete by keys", ref ex))
                    throw ex;
                throw;
            }
        }

        public TEntityId Get(Expression<Func<TEntityId, bool>> where)
        {
            try
            {
                return DbSet.Where(where).FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Find by expression", ref ex))
                    throw ex;
                throw;
            }
        }

        public TEntityId Get(params object[] Keys)
        {
            try
            {
                return DbSet.Find(Keys);
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Find by keys", ref ex))
                    throw ex;
                throw;
            }
        }

        public IQueryable<TEntityId> Query()
        {
            try
            {
                return DbSet;
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Query", ref ex))
                    throw ex;
                throw;
            }
        }

        public IQueryable<TEntityId> Query(params Expression<Func<TEntityId, object>>[] includes)
        {
            try
            {
                IQueryable<TEntityId> Set = Query();
                foreach (var include in includes)
                {
                    Set = Set.Include(include);
                }
                return Set;
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Query by expression", ref ex))
                    throw ex;
                throw;
            }
        }

        public IQueryable<TEntityId> QueryFast()
        {
            try
            {
                return DbSet.AsNoTracking<TEntityId>();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "QueryFast", ref ex))
                    throw ex;
                throw;
            }
        }

        public IQueryable<TEntityId> QueryFast(params Expression<Func<TEntityId, object>>[] includes)
        {
            try
            {
                IQueryable<TEntityId> Set = QueryFast();
                foreach (var include in includes)
                {
                    Set = Set.Include(include);
                }
                return Set.AsNoTracking();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "QueryFast by expression", ref ex))
                    throw ex;
                throw;
            }
        }
    }
}
