using Loris.Common.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loris.Common.Domain.Interfaces
{
    public interface IApplicationService<TDto> where TDto : class
    {
        Task<TreatedResult<IEnumerable<TDto>>> Get();

        Task<TreatedResult<PageResult<TDto>>> Get(RequestParameter param);

        Task<TreatedResult<TDto>> Get(object id);

        Task<TreatedResult<TDto>> Add(TDto dto);

        Task<TreatedResult> Update(TDto dto, int anotherUserSecondsToChange);

        Task<TreatedResult> Delete(object id);
    }
}
