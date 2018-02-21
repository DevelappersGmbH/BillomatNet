using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaurusSoftware.BillomatNet.Queries;
using TaurusSoftware.BillomatNet.Types;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class QueryString
    {
        internal static string For(Query<Client, ClientFilter> value)
        {
            if (value == null)
            {
                return null;
            }

            var filter = value.Filter.ToQueryString();
            var sort = value.Sort.ToQueryString();
            var paging = value.Paging.ToQueryString();

            return string.Join("&", new[] { filter, sort, paging }.AsEnumerable().Where(x => !string.IsNullOrEmpty(x)));
        }

        internal static string For(Query<Article, ArticleFilter> value)
        {
            if (value == null)
            {
                return null;
            }

            var filter = value.Filter.ToQueryString();
            var sort = value.Sort.ToQueryString();
            var paging = value.Paging.ToQueryString();

            return string.Join("&", new[] { filter, sort, paging }.AsEnumerable().Where(x => !string.IsNullOrEmpty(x)));
        }

        internal static string ToQueryString(this List<SortItem<Client>> value)
        {
            if (value == null ||value.Count == 0)
            {
                return null;
            }

            var sortItems = value.Select(x =>
            {
                var domainObjectName = ReflectionHelper.GetPropertyInfo(x.Property).Name;
                var queryMemberName = (typeof(Api.Client)
                    .GetProperty(domainObjectName)
                    .GetCustomAttributes(typeof(JsonPropertyAttribute), true)
                    .FirstOrDefault() as JsonPropertyAttribute)?.PropertyName;
                var order = x.Order == SortOrder.Descending ? "ASC" : "DESC";
                return HttpUtility.UrlEncode($"{queryMemberName} {order}");
            });

            
            return "order_by=" + string.Join(",", sortItems);
        }

        internal static string ToQueryString(this List<SortItem<Article>> value)
        {
            if (value == null || value.Count == 0)
            {
                return null;
            }

            var sortItems = value.Select(x =>
            {
                var domainObjectName = ReflectionHelper.GetPropertyInfo(x.Property).Name;
                var queryMemberName = (typeof(Api.Article)
                    .GetProperty(domainObjectName)
                    .GetCustomAttributes(typeof(JsonPropertyAttribute), true)
                    .FirstOrDefault() as JsonPropertyAttribute)?.PropertyName;
                var order = x.Order == SortOrder.Descending ? "ASC" : "DESC";
                return HttpUtility.UrlEncode($"{queryMemberName} {order}");
            });


            return "order_by=" + string.Join(",", sortItems);
        }


        internal static string ToQueryString(this PagingSettings value)
        {
            if (value == null)
            {
                return null;
            }

            return $"per_page={value.ItemsPerPage}&page={value.Page}";
        }

        internal static string ToQueryString(this ArticleFilter value)
        {
            // TODO
            return "";
        }

        internal static string ToQueryString(this ClientFilter value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(value.Name))
            {
                filters.Add($"name={HttpUtility.UrlEncode(value.Name)}");
            }

            if (!string.IsNullOrEmpty(value.ClientNumber))
            {
                filters.Add($"client_number={HttpUtility.UrlEncode(value.ClientNumber)}");
            }

            if (!string.IsNullOrEmpty(value.Email))
            {
                filters.Add($"email={HttpUtility.UrlEncode(value.Email)}");
            }

            if (!string.IsNullOrEmpty(value.FirstName))
            {
                filters.Add($"first_name={HttpUtility.UrlEncode(value.FirstName)}");
            }

            if (!string.IsNullOrEmpty(value.LastName))
            {
                filters.Add($"last_name={HttpUtility.UrlEncode(value.LastName)}");
            }

            if (!string.IsNullOrEmpty(value.LastName))
            {
                filters.Add($"last_name={HttpUtility.UrlEncode(value.LastName)}");
            }

            if (!string.IsNullOrEmpty(value.CountryCode))
            {
                filters.Add($"country_code={HttpUtility.UrlEncode(value.CountryCode)}");
            }

            if (!string.IsNullOrEmpty(value.Note))
            {
                filters.Add($"note={HttpUtility.UrlEncode(value.Note)}");
            }

            if ((value.InvoiceIds?.Count ?? 0) > 0)
            {
                filters.Add($"invoice_id={string.Join(",", value.InvoiceIds)}");
            }

            if ((value.Tags?.Count ?? 0) > 0)
            {
                filters.Add($"invoice_id={string.Join(",", value.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
        }
    }
}