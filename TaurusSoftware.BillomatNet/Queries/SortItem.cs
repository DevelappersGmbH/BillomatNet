using System;
using System.Linq.Expressions;

namespace TaurusSoftware.BillomatNet.Queries
{
    public class SortItem<TSource> : ISortItem<TSource>
    {
        public Expression<Func<TSource, object>> Property { get; set; }
        public SortOrder Order { get; set; }
    }

    public interface ISortItem<T> { }
}