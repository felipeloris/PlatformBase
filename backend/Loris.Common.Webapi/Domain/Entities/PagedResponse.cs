using Loris.Common.Domain.Entities;

namespace Loris.Common.Webapi.Domain.Entities
{
    public class PagedResponse<T> : TreatedResult<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize) : base(TreatedResultStatus.Success, data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
