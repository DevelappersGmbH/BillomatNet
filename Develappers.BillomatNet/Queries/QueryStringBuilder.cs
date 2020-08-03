// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Mapping;
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

        protected internal virtual string GetFilterStringForProperty(TFilter filter, string propertyName)
        {
            return null;
        }

        public string GetFilterStringFor(TFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            var filterProperties = typeof(TFilter).GetProperties();
            var apiProperties = typeof(TApiEntity).GetProperties();
            foreach (var filterProperty in filterProperties)
            {
                var filterValue = filterProperty.GetMethod.Invoke(filter, null);
                if (filterValue == null)
                {
                    continue;
                }

                // first let the child implementation decide
                var filterString = GetFilterStringForProperty(filter, filterProperty.Name);
                if (!string.IsNullOrEmpty(filterString))
                {
                    filters.Add(filterString);
                    continue;
                }

                // the child implementation didn't return any filter - so let's try the standard implementation
                var apiProperty = apiProperties.Single(x => x.Name == filterProperty.Name);
                if (apiProperty == null)
                {
                    continue;
                }

                var apiName = apiProperty.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName;
                if (apiName == null)
                {
                    continue;
                }

                if (filterProperty.GetMethod.ReturnType == typeof(string))
                {
                    filterString = HttpUtility.UrlEncode((string)filterValue);
                }
                else if (filterProperty.GetMethod.ReturnType == typeof(bool) || filterProperty.GetMethod.ReturnType == typeof(bool?))
                {
                    filterString = ((bool)filterValue).BoolToString();
                }
                else if (filterProperty.GetMethod.ReturnType == typeof(int) || filterProperty.GetMethod.ReturnType == typeof(int?))
                {
                    filterString = ((int)filterValue).ToString(CultureInfo.InvariantCulture);
                }
                else if (filterProperty.GetMethod.ReturnType == typeof(long) || filterProperty.GetMethod.ReturnType == typeof(long?))
                {
                    filterString = ((long)filterValue).ToString(CultureInfo.InvariantCulture);
                }
                else if (filterProperty.GetMethod.ReturnType == typeof(short) || filterProperty.GetMethod.ReturnType == typeof(short?))
                {
                    filterString = ((short)filterValue).ToString(CultureInfo.InvariantCulture);
                }
                else if (filterProperty.GetMethod.ReturnType == typeof(byte) || filterProperty.GetMethod.ReturnType == typeof(byte?))
                {
                    filterString = ((byte)filterValue).ToString(CultureInfo.InvariantCulture);
                }
                else if (filterProperty.GetMethod.ReturnType == typeof(sbyte) || filterProperty.GetMethod.ReturnType == typeof(sbyte?))
                {
                    filterString = ((sbyte)filterValue).ToString(CultureInfo.InvariantCulture);
                }

                filters.Add($"{apiName}={filterString}");
            }

            return string.Join("&", filters);
        }

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
