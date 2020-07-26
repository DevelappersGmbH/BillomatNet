// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Web;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class UnitQueryStringBuilder : QueryStringBuilder<Unit, Api.Unit, UnitFilter>
    {
        protected internal override string GetFilterStringFor(UnitFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                filters.Add($"?name={HttpUtility.UrlEncode(filter.Name)}");
            }

            return string.Join("&", filters);
        }
    }
}
