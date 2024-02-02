// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class TemplateQueryStringBuilder : QueryStringBuilder<Template, Api.Template, TemplateFilter>
    {
        protected internal override string GetFilterStringFor(TemplateFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(filter.Type))
            {
                filters.Add($"type={HttpUtility.UrlEncode(filter.Type)}");
            }
            return string.Join("&", filters);
        }
    }
}
