using System;
using System.Linq.Expressions;
using TaurusSoftware.BillomatNet.Model;

namespace TaurusSoftware.BillomatNet
{
    public class SortItem<T>
    {
        public Expression<Func<T, object>> Property { get; set; }
        public SortOrder Order { get; set; }
    }
}