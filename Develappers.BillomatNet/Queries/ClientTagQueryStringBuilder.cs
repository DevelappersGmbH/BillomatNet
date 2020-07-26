// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class ClientTagQueryStringBuilder : QueryStringBuilder<ClientTag, Api.ClientTag, ClientTagFilter>
    {
        protected internal override string GetFilterStringFor(ClientTagFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            filters.Add($"client_id={filter.ClientId}");

            return string.Join("&", filters);
        }
    }
}
