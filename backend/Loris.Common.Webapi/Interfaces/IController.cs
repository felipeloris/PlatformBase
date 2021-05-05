using Loris.Common.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loris.Common.Webapi.Interfaces
{
    public interface IController<T>
        where T : class
    {
        Task<ActionResult<IEnumerable<T>>> Get();

        Task<ActionResult<PageResult<T>>> GetByParameters(RequestParameter parameters);

        Task<ActionResult<T>> Get(int id);

        Task<ActionResult> Post(T obj);

        Task<ActionResult> Put(int anotherUserSecondsToChange, T obj);

        Task<ActionResult> Delete(int id);
    }
}
