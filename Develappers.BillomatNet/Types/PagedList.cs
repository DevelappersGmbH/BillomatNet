using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    public class PagedList<T>
    {
        public List<T> List { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }
    }
}