// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Develappers.BillomatNet.Helpers;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Queries
{
    internal abstract class QueryStringBuilder<TDomainEntity, TApiEntity, TFilter> where TFilter : new()
    {
        public string BuildFor(Query<TDomainEntity, TFilter> value)
        {
            if (value == null)
            {
                return null;
            }

            var filter = GetFilterStringFor(value.Filter);
            var sort = GetSortStringFor(value.Sort);
            var paging = GetPagingStringFor(value.Paging);

            return string.Join("&", new[] { filter, sort, paging }.AsEnumerable().Where(x => !string.IsNullOrEmpty(x)));
        }

        protected internal abstract string GetFilterStringFor(TFilter filter);

        protected internal virtual string GetSortStringFor(List<SortItem<TDomainEntity>> sort)
        {
            return GetSortQueryStringFor(sort);
        }
        protected internal virtual string GetPagingStringFor(PagingSettings paging)
        {
            if (paging == null)
            {
                return null;
            }

            return $"per_page={paging.ItemsPerPage}&page={paging.Page}";
        }

        private static string GetSortQueryStringFor(List<SortItem<TDomainEntity>> value)
        {
            if (value == null || value.Count == 0)
            {
                return null;
            }

            var sortItems = value.Select(x =>
            {
                var domainObjectName = ReflectionHelper.GetPropertyInfo(x.Property).Name;
                var queryMemberName = (typeof(TApiEntity)
                    .GetProperty(domainObjectName)
                    .GetCustomAttributes(typeof(JsonPropertyAttribute), true)
                    .FirstOrDefault() as JsonPropertyAttribute)?.PropertyName;
                var order = x.Order == SortOrder.Descending ? "DESC" : "ASC";
                return HttpUtility.UrlEncode($"{queryMemberName} {order}");
            });

            return "order_by=" + string.Join(",", sortItems);
        }
    }
}
