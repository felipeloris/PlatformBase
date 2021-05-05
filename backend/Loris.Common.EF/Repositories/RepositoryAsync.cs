using Loris.Common.Domain.Interfaces;
using Loris.Common.Exceptions;
using Loris.Common.Extensions;
using Loris.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loris.Common.EF.Repository
{
    public abstract class RepositoryAsync<TEntityId, TContext> :
        RepositoryAsyncBase<TEntityId, TContext>, IRepositoryAsync<TEntityId>
        where TEntityId : class, IEntityIdBase
        where TContext : DbContext
    {
        private Type TypeOfEntity = typeof(TEntityId);

        public RepositoryAsync(IDatabase database) : base(database) { }

        public RepositoryAsync(TContext context) : base(context) { }

        protected virtual async Task<TEntityId> InternalGetById(object id)
        {
            var newId = EntityIdHelper.ConvertId(TypeOfEntity, id);

            return await DbSet.FindAsync(newId);
        }

        public virtual async Task<TEntityId> GetById(object id)
        {
            try
            {
                return await InternalGetById(id);
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "GetById", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<int> Update(TEntityId obj)
        {
            try
            {
                var id = obj.GetId();
                DetachLocal(dl => dl.GetId().Equals(id));

                dbContext.Entry(obj).State = EntityState.Modified;
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "Update", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<int> UpdateRange(IEnumerable<TEntityId> entities)
        {
            try
            {
                //var x = entities.Select(x => DetachLocal(dl => dl.GetId().Equals(x.GetId())));

                DbSet.UpdateRange(entities);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "UpdateRange", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<int> DeleteById(object id)
        {
            try
            {
                var entity = await InternalGetById(id);
                
                if (entity == null) 
                    return 0;

                return await base.Delete(entity);
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "DeleteById", ref ex))
                    throw ex;
                throw;
            }
        }
    }
}
