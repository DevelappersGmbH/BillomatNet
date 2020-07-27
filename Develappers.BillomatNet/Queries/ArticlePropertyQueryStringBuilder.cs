// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Web;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class ArticlePropertyQueryStringBuilder : QueryStringBuilder<ArticleProperty, Api.ArticleProperty, ArticlePropertyFilter>
    {
        protected internal override string GetFilterStringFor(ArticlePropertyFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (filter.ArticleId.HasValue)
            {
                filters.Add($"article_id={filter.ArticleId.Value}");
            }

            if (filter.ArticlePropertyId.HasValue)
            {
                filters.Add($"article_property_id={filter.ArticlePropertyId.Value}");
            }

            if (filter.Value != null)
            {
                string val;
                if (filter.Value is bool)
                {
                    val = filter.Value.ToString();
                }
                else
                {
                    val = (string)filter.Value;
                }

                filters.Add($"value={HttpUtility.UrlEncode(val)}");
            }

            return string.Join("&", filters);
        }
    }
}
