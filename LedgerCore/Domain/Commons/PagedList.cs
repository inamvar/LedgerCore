using System.Collections.Generic;

namespace LedgerCore.Domain.Commons
{
    public class PagedList<T> : List<T>, IPagedEnumareable<T>
        where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }


    }
}
