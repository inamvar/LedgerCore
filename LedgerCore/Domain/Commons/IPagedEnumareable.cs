using System.Collections.Generic;

namespace LedgerCore.Domain.Commons
{
    public interface IPagedEnumareable<T> : IEnumerable<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
    }
}
