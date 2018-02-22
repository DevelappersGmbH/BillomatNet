using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaurusSoftware.BillomatNet.Helpers;

namespace TaurusSoftware.BillomatNet.Queries
{ 
    public class Query<TBase, TFilter> where TFilter: new()
    {
        public Query()
        {
            Paging = new PagingSettings();
            Filter = new TFilter();
            Sort = new List<SortItem<TBase>>();
        }

        public TFilter Filter { get; }

        public List<SortItem<TBase>> Sort { get; }

        public PagingSettings Paging { get; }

        public Query<TBase, TFilter> SetPage(int value)
        {
            Paging.Page = value;
            return this;
        }

        public Query<TBase, TFilter> SetItemsPerPage(int value)
        {
            Paging.ItemsPerPage = value;
            return this;
        }

        public Query<TBase, TFilter> AddFilter(Expression<Func<TFilter, object>> property, object value)
        {
            var propertyInfo = ReflectionHelper.GetPropertyInfo(property);
            propertyInfo.GetSetMethod().Invoke(this.Filter, new[] {value});
            return this;
        }

        public Query<TBase, TFilter> AddSort(Expression<Func<TBase, object>> property, SortOrder order = SortOrder.Ascending)
        {
            Sort.Add(new SortItem<TBase>{Order = order, Property = property});
            return this;
        }

    }
}