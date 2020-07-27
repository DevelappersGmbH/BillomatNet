// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class ArticleQueryStringBuilder : QueryStringBuilder<Article, Api.Article, ArticleFilter>
    {
        protected internal override string GetFilterStringFor(ArticleFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(filter.ArticleNumber))
            {
                filters.Add($"article_number={HttpUtility.UrlEncode(filter.ArticleNumber)}");
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                filters.Add($"title={HttpUtility.UrlEncode(filter.Title)}");
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                filters.Add($"description={HttpUtility.UrlEncode(filter.Description)}");
            }

            if (!string.IsNullOrEmpty(filter.CurrencyCode))
            {
                filters.Add($"currency_code={HttpUtility.UrlEncode(filter.CurrencyCode)}");
            }

            if (filter.SupplierId.HasValue)
            {
                filters.Add($"supplier_id={filter.SupplierId.Value}");
            }

            if (filter.UnitId.HasValue)
            {
                filters.Add($"unit_id={filter.UnitId.Value}");
            }

            if ((filter.Tags?.Count ?? 0) > 0)
            {
                filters.Add($"tags={string.Join(",", filter.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
        }
    }
}
