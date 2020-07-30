using System;
using System.Collections.Generic;
using System.Text;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class InvoiceCommentQueryStringBuilder : QueryStringBuilder<InvoiceComment, Api.InvoiceComment, InvoiceCommentFilter>
    {
        protected internal override string GetFilterStringFor(InvoiceCommentFilter filter)
        {
            if (filter == null || !filter.InvoiceId.HasValue)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (filter.InvoiceId.HasValue)
            {
                filters.Add($"invoice_id={filter.InvoiceId.Value}");
            }
            if (filter.ActionKey != null)
            {
                var filterValue = string.Join(", ", filter.ActionKey).ToUpper();
                filters.Add($"actionkey={filterValue}");
            }
            return string.Join("&", filters);
        }
    }
}
