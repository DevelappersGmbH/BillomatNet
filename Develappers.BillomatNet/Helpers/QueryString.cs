// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Helpers
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

        internal static string For(Query<ClientTag, ClientTagFilter> value)
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

        internal static string For(Query<ClientProperty, ClientPropertyFilter> value)
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

        internal static string For(Query<ArticleTag, ArticleTagFilter> value)
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

        internal static string For(Query<Invoice, InvoiceFilter> value)
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

        internal static string For(Query<ArticleProperty, ArticlePropertyFilter> value)
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

        internal static string For(Query<Unit, UnitFilter> value)
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

        internal static string ToQueryString<TDomain, TApi>(this List<SortItem<TDomain>> value)
        {
            if (value == null || value.Count == 0)
            {
                return null;
            }

            var sortItems = value.Select(x =>
            {
                var domainObjectName = ReflectionHelper.GetPropertyInfo(x.Property).Name;
                var queryMemberName = (typeof(TApi)
                    .GetProperty(domainObjectName)
                    .GetCustomAttributes(typeof(JsonPropertyAttribute), true)
                    .FirstOrDefault() as JsonPropertyAttribute)?.PropertyName;
                var order = x.Order == SortOrder.Descending ? "DESC" : "ASC";
                return HttpUtility.UrlEncode($"{queryMemberName} {order}");
            });


            return "order_by=" + string.Join(",", sortItems);
        }

        internal static string ToQueryString(this List<SortItem<Client>> value)
        {
            return ToQueryString<Client, Api.Client>(value);
        }

        internal static string ToQueryString(this List<SortItem<ClientTag>> value)
        {
            return ToQueryString<ClientTag, Api.ClientTag>(value);
        }

        internal static string ToQueryString(this List<SortItem<ClientProperty>> value)
        {
            return ToQueryString<ClientProperty, Develappers.BillomatNet.Api.ClientProperty>(value);
        }

        internal static string ToQueryString(this List<SortItem<Invoice>> value)
        {
            return ToQueryString<Invoice, Api.Invoice>(value);
        }

        internal static string ToQueryString(this List<SortItem<Article>> value)
        {
            return ToQueryString<Article, Develappers.BillomatNet.Api.Article>(value);
        }

        internal static string ToQueryString(this List<SortItem<ArticleProperty>> value)
        {
            return ToQueryString<ArticleProperty, Develappers.BillomatNet.Api.ArticleProperty>(value);
        }

        internal static string ToQueryString(this List<SortItem<ArticleTag>> value)
        {
            return ToQueryString<ArticleTag, Api.ArticleTag>(value);
        }
        internal static string ToQueryString(this UnitFilter value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(value.Name))
            {
                filters.Add($"?name={HttpUtility.UrlEncode(value.Name)}");
            }

            return string.Join("&", filters);
        }
       
        internal static string ToQueryString(this List<SortItem<Unit>> value)
        {
            return ToQueryString<Unit, Develappers.BillomatNet.Api.Unit>(value);
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
            if (value == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(value.ArticleNumber))
            {
                filters.Add($"article_number={HttpUtility.UrlEncode(value.ArticleNumber)}");
            }

            if (!string.IsNullOrEmpty(value.Title))
            {
                filters.Add($"title={HttpUtility.UrlEncode(value.Title)}");
            }

            if (!string.IsNullOrEmpty(value.Description))
            {
                filters.Add($"description={HttpUtility.UrlEncode(value.Description)}");
            }

            if (!string.IsNullOrEmpty(value.CurrencyCode))
            {
                filters.Add($"currency_code={HttpUtility.UrlEncode(value.CurrencyCode)}");
            }

            if (value.SupplierId.HasValue)
            {
                filters.Add($"supplier_id={value.SupplierId.Value}");
            }

            if (value.UnitId.HasValue)
            {
                filters.Add($"unit_id={value.UnitId.Value}");
            }

            if ((value.Tags?.Count ?? 0) > 0)
            {
                filters.Add($"tags={string.Join(",", value.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
        }

        internal static string ToQueryString(this ClientTagFilter value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            filters.Add($"client_id={value.ClientId}");

            return string.Join("&", filters);
        }

        internal static string ToQueryString(this ClientPropertyFilter value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (value.ClientId.HasValue)
            {
                filters.Add($"client_id={value.ClientId.Value}");
            }

            if (value.ClientPropertyId.HasValue)
            {
                filters.Add($"client_property_id={value.ClientPropertyId.Value}");
            }

            if (value.Value != null)
            {
                string val;
                if (value.Value is bool)
                {
                    val = value.Value.ToString();
                }
                else
                {
                    val = (string)value.Value;
                }

                filters.Add($"value={HttpUtility.UrlEncode(val)}");
            }

            return string.Join("&", filters);
        }

        internal static string ToQueryString(this ArticlePropertyFilter value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (value.ArticleId.HasValue)
            {
                filters.Add($"article_id={value.ArticleId.Value}");
            }

            if (value.ArticlePropertyId.HasValue)
            {
                filters.Add($"article_property_id={value.ArticlePropertyId.Value}");
            }

            if (value.Value != null)
            {
                string val;
                if (value.Value is bool)
                {
                    val = value.Value.ToString();
                }
                else
                {
                    val = (string)value.Value;
                }

                filters.Add($"value={HttpUtility.UrlEncode(val)}");
            }

            return string.Join("&", filters);
        }

        internal static string ToQueryString(this ArticleTagFilter value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            filters.Add($"article_id={value.ArticleId}");

            return string.Join("&", filters);
        }

        internal static string ToQueryString(this InvoiceFilter value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (value.ClientId.HasValue)
            {
                filters.Add($"client_id={value.ClientId.Value}");
            }

            if (value.ContactId.HasValue)
            {
                filters.Add($"contact_id={value.ContactId.Value}");
            }

            if (!string.IsNullOrEmpty(value.InvoiceNumber))
            {
                filters.Add($"invoice_number={HttpUtility.UrlEncode(value.InvoiceNumber)}");
            }

            if ((value.Status?.Count ?? 0) > 0)
            {
                filters.Add($"status={string.Join(",", value.Status.Select(CommonMappingExtensions.ToApiValue))}");
            }

            if ((value.PaymentType?.Count ?? 0) > 0)
            {
                filters.Add($"payment_type={string.Join(",", value.PaymentType.Select(CommonMappingExtensions.ToApiValue))}");
            }

            if (value.From.HasValue)
            {
                filters.Add($"from={value.From.Value:YYYY-mm-DD}");
            }

            if (value.To.HasValue)
            {
                filters.Add($"from={value.To.Value:YYYY-mm-DD}");
            }

            if (!string.IsNullOrEmpty(value.Label))
            {
                filters.Add($"label={HttpUtility.UrlEncode(value.Label)}");
            }

            if (!string.IsNullOrEmpty(value.Intro))
            {
                filters.Add($"intro={HttpUtility.UrlEncode(value.Intro)}");
            }

            if (!string.IsNullOrEmpty(value.Note))
            {
                filters.Add($"note={HttpUtility.UrlEncode(value.Note)}");
            }

            if ((value.Tags?.Count ?? 0) > 0)
            {
                filters.Add($"tags={string.Join(",", value.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
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
                filters.Add($"tags={string.Join(",", value.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
        }
    }
}
