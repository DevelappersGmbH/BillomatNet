using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TaurusSoftware.BillomatNet.Model;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class QueryString
    {
        internal static string For(ClientFilterSortOptions value)
        {
            if (value == null)
            {
                return null;
            }

            var filter = value.Filter?.ToQueryString();
            var sort = value.Sort?.ToQueryString();
            var paging = value.Paging.ToQueryString();

            return string.Join("&", new[] { filter, sort, paging }.AsEnumerable().Where(x => !string.IsNullOrEmpty(x)));
        }


        internal static string ToQueryString(this ClientSortSettings value)
        {
            if (value == null ||value.Count == 0)
            {
                return null;
            }

            var sortItems = value.Select(x =>
            {
                var domainObjectName = GetMemberInfo(x.Property).Member.Name;
                var queryMemberName = (typeof(Api.Client)
                    .GetProperty(domainObjectName)
                    .GetCustomAttributes(typeof(JsonPropertyAttribute), true)
                    .FirstOrDefault() as JsonPropertyAttribute)?.PropertyName;
                var order = x.Order == SortOrder.Descending ? "ASC" : "DESC";
                return HttpUtility.UrlEncode($"{queryMemberName} {order}");
            });

            
            return "order_by=" + string.Join(",", sortItems);
        }

        private static MemberExpression GetMemberInfo(Expression method)
        {
            LambdaExpression lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }


        internal static string ToQueryString(this PagingSettings value)
        {
            if (value == null)
            {
                return null;
            }

            return $"per_page={value.ItemsPerPage}&page={value.Page}";
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