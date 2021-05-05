using System.Collections.Generic;

namespace Loris.Common.Domain.Entities
{
    public class PageResult<T> : PagingControl
        where T : class
    {
        public IList<T> Results { get; set; }

        public PageResult(int count, int pageNumber, int pageSize)
            : base(count, pageNumber, pageSize)
        {
        }
    }
}
