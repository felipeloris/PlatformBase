using Loris.Common.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loris.Common.Domain.Interfaces
{
    public interface IServiceAsync<TEntityId> where TEntityId : class, IEntityIdBase
    {
        Task<IEnumerable<TEntityId>> Get();

        Task<PageResult<TEntityId>> Get(RequestParameter param);

        Task<TEntityId> GetById(object id);

        Task<TEntityId> Add(TEntityId obj);

        Task Update(TEntityId obj, int anotherUserSecondsToChange);

        Task Delete(object id);
    }
}
