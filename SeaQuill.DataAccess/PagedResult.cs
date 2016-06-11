using System.Collections.Generic;

namespace SeaQuill.DataAccess
{
    public class PagedResult<T>
    {
        public ICollection<T> PagedData { get; }
        public int TotalCount { get; }

        public int Count => PagedData.Count;

        public PagedResult(ICollection<T> pagedData, int totalCount)
        {
            PagedData = pagedData;
            TotalCount = totalCount;
        }
    }
}
