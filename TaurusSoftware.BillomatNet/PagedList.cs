using System.Collections.Generic;

namespace TaurusSoftware.BillomatNet
{
    public class PagedList<T>
    {
        public List<T> List { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
    }
}