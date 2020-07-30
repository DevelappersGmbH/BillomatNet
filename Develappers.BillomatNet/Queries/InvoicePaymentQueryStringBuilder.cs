// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class InvoicePaymentQueryStringBuilder : QueryStringBuilder<InvoicePayment, Api.InvoicePayment, InvoicePaymentFilter>
    {
        protected internal override string GetFilterStringFor(InvoicePaymentFilter filter)
        {
            var filters = new List<string>();

            if (filter.InvoiceId != null)
            {
                filters.Add($"invoice_id={filter.InvoiceId.Value}");
            }
            if (filter.From != null)
            {
                filters.Add($"from={filter.From.DateTimeToDateString()}");
            }
            if (filter.From != null)
            {
                filters.Add($"to={filter.To.DateTimeToDateString()}");
            }
            if (filter.Type != null)
            {
                var filterValue = string.Join(", ", filter.Type).ToUpper();
                filters.Add($"type={filterValue}");
            }
            if (filter.UserId != null)
            {
                filters.Add($"user_id={filter.UserId.Value}");
            }
            return string.Join("&", filters);
        }
    }
}
