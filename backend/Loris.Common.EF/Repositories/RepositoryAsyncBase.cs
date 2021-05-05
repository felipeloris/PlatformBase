using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.EF.Helpers;
using Loris.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loris.Common.EF.Repository
{
    public abstract class RepositoryAsyncBase<TEntityId, TContext> : 
        RepositoryBase<TEntityId, TContext>
        where TEntityId : class, IEntityIdBase
        where TContext : DbContext
    {
        public RepositoryAsyncBase() : base() { }

        public RepositoryAsyncBase(IDatabase database) : base(database) { }

        public RepositoryAsyncBase(TContext context) : base(context) { }

        protected virtual void DetachLocal(Func<TEntityId, bool> predicate)
        {
            var local = dbContext.Set<TEntityId>().Local.Where(predicate).FirstOrDefault();
            if (local != null)
                dbContext.Entry(local).State = EntityState.Detached;
        }

        public virtual async Task<TEntityId> Add(TEntityId obj)
        {
            try
            {
                var r = await DbSet.AddAsync(obj);
                await dbContext.SaveChangesAsync();
                return r.Entity;
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Add", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<int> AddRange(IEnumerable<TEntityId> objs)
        {
            try
            {
                await DbSet.AddRangeAsync(objs);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Add range", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<List<TEntityId>> Get(Expression<Func<TEntityId, bool>> where)
        {
            try
            {
                return await DbSet.Where(where).ToListAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Get by expression", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<PageResult<TEntityId>> Get(RequestParameter param)
        {
            return await PaginatedListHelper.GetAsync(DbSet, param);
        }

        public virtual async Task<List<TEntityId>> GetAll()
        {
            try
            {
                //return await Task.FromResult(dbSet);
                return await DbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "GetAll", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<int> Delete(TEntityId obj)
        {
            try
            {
                DbSet.Remove(obj);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Delete object", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<int> Delete(Expression<Func<TEntityId, bool>> where)
        {
            try
            {
                List<TEntityId> list = await Get(where);
                DbSet.RemoveRange(list);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Delete by expression", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<int> DeleteRange(IEnumerable<TEntityId> entities)
        {
            try
            {
                DbSet.RemoveRange(entities);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Delete by expression", ref ex))
                    throw ex;
                throw;
            }
        }
    }
}
