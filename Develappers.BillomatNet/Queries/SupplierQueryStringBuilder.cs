// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class SupplierQueryStringBuilder : QueryStringBuilder<Supplier, Api.Supplier, SupplierFilter>
    {
        protected internal override string GetFilterStringFor(SupplierFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                filters.Add($"name={HttpUtility.UrlEncode(filter.Name)}");
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                filters.Add($"email={HttpUtility.UrlEncode(filter.Email)}");
            }

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                filters.Add($"first_name={HttpUtility.UrlEncode(filter.FirstName)}");
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                filters.Add($"last_name={HttpUtility.UrlEncode(filter.LastName)}");
            }

            if (!string.IsNullOrEmpty(filter.CountryCode))
            {
                filters.Add($"country_code={HttpUtility.UrlEncode(filter.CountryCode)}");
            }

            if (!string.IsNullOrEmpty(filter.CreditorIdentifier))
            {
                filters.Add($"creditor_identifier={HttpUtility.UrlEncode(filter.CreditorIdentifier)}");
            }

            if (!string.IsNullOrEmpty(filter.Note))
            {
                filters.Add($"note={HttpUtility.UrlEncode(filter.Note)}");
            }

            if (!string.IsNullOrEmpty(filter.ClientNumber))
            {
                filters.Add($"client_number={HttpUtility.UrlEncode(filter.ClientNumber)}");
            }

            if ((filter.IncomingIds?.Count ?? 0) > 0)
            {
                filters.Add($"incoming_id={string.Join(",", filter.IncomingIds)}");
            }

            if ((filter.Tags?.Count ?? 0) > 0)
            {
                filters.Add($"tags={string.Join(",", filter.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
        }
    }
}
