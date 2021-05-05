using System;

namespace Loris.Common.Domain.Entities
{
    public class PagingControl
    {
        public int Count { get; private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }

        public int NextRecord { get; private set; }

        public PagingControl(int count, int pageNumber, int pageSize)
        {
            Count = count;
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            NextRecord = (pageNumber - 1) * pageSize;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
