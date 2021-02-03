// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Web;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class ClientPropertyQueryStringBuilder : QueryStringBuilder<ClientProperty, Api.ClientProperty, ClientPropertyFilter>
    {
        protected internal override string GetFilterStringFor(ClientPropertyFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (filter.ClientId.HasValue)
            {
                filters.Add($"client_id={filter.ClientId.Value}");
            }

            if (filter.ClientPropertyId.HasValue)
            {
                filters.Add($"client_property_id={filter.ClientPropertyId.Value}");
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
