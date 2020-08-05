// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class InvoiceTagQueryStringBuilder : QueryStringBuilder<InvoiceTag, Api.InvoiceTag, InvoiceTagFilter>
    {
        protected internal override string GetFilterStringFor(InvoiceTagFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            filters.Add($"invoice_id={filter.InvoiceId}");

            return string.Join("&", filters);
        }
    }
}
