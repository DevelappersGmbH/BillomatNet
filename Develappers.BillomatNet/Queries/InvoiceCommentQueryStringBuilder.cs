// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class InvoiceCommentQueryStringBuilder : QueryStringBuilder<InvoiceComment, Api.InvoiceComment, InvoiceCommentFilter>
    {
        protected internal override string GetFilterStringFor(InvoiceCommentFilter filter)
        {
            if (filter?.InvoiceId == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();

            filters.Add($"invoice_id={filter.InvoiceId.Value}");

            if (filter.ActionKey != null)
            {
                var filterValue = string.Join(",", filter.ActionKey.Select(x => x.ToApiValue()));
                filters.Add($"actionkey={filterValue}");
            }
            return string.Join("&", filters);
        }
    }
}
