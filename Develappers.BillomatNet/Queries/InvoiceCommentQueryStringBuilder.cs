// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class InvoiceCommentQueryStringBuilder : QueryStringBuilder<InvoiceComment, Api.InvoiceComment, InvoiceCommentFilter>
    {
        // TODO: test
        //protected internal override string GetFilterStringFor(InvoiceCommentFilter filter)
        //{
        //    if (filter?.InvoiceId == null)
        //    {
        //        return string.Empty;
        //    }

        //    var filters = new List<string>();

        //    filters.Add($"invoice_id={filter.InvoiceId.Value}");

        //    if (filter.ActionKey != null)
        //    {
        //        var filterValue = string.Join(", ", filter.ActionKey).ToUpper();
        //        filters.Add($"actionkey={filterValue}");
        //    }
        //    return string.Join("&", filters);
        //}

        protected internal override string GetFilterStringForProperty(InvoiceCommentFilter filter, string propertyName)
        {
            switch (propertyName)
            {
                case nameof(filter.ActionKey):
                    var filterValue = string.Join(", ", filter.ActionKey).ToUpper();
                    return $"actionkey={filterValue}";
            }

            return base.GetFilterStringForProperty(filter, propertyName);
        }
    }
}
