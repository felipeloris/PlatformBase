using Loris.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loris.Common.Domain.Interfaces
{
    public interface IRepositoryAsync<TEntityId> where TEntityId : class, IEntityIdBase
    {
        Task<TEntityId> GetById(object id);

        Task<List<TEntityId>> Get(Expression<Func<TEntityId, bool>> where);

        Task<PageResult<TEntityId>> Get(RequestParameter param);

        Task<List<TEntityId>> GetAll();

        Task<TEntityId> Add(TEntityId obj);

        Task<int> AddRange(IEnumerable<TEntityId> entities);

        Task<int> Update(TEntityId obj);

        Task<int> UpdateRange(IEnumerable<TEntityId> entities);

        Task<int> DeleteById(object id);

        Task<int> Delete(TEntityId obj);

        Task<int> Delete(Expression<Func<TEntityId, bool>> where);

        Task<int> DeleteRange(IEnumerable<TEntityId> entities);
    }
}
