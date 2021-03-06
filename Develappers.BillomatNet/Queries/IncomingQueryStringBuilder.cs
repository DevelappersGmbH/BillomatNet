﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class IncomingQueryStringBuilder : QueryStringBuilder<PurchaseInvoice, Api.Incoming, PurchaseInvoiceFilter>
    {
        protected internal override string GetFilterStringFor(PurchaseInvoiceFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();

            if (filter.SupplierId.HasValue)
            {
                filters.Add($"supplier_id={filter.SupplierId.Value}");
            }

            if (!string.IsNullOrEmpty(filter.Number))
            {
                filters.Add($"incoming_number={HttpUtility.UrlEncode(filter.Number)}");
            }

            if ((filter.Status?.Count ?? 0) > 0)
            {
                filters.Add($"status={string.Join(",", filter.Status.Select(MappingHelpers.ToApiValue))}");
            }

            if (filter.From.HasValue)
            {
                filters.Add($"from={filter.From.Value:yyyy-MM-dd}");
            }

            if (filter.To.HasValue)
            {
                filters.Add($"to={filter.To.Value:yyyy-MM-dd}");
            }

            if (!string.IsNullOrEmpty(filter.Note))
            {
                filters.Add($"note={HttpUtility.UrlEncode(filter.Note)}");
            }

            if ((filter.Tags?.Count ?? 0) > 0)
            {
                filters.Add($"tags={string.Join(",", filter.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
        }
    }
}
