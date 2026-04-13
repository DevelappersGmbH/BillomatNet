// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class FreeTextQueryStringBuilder : QueryStringBuilder<FreeText, Api.FreeText, FreeTextFilter>
    {
        protected internal override string GetFilterStringFor(FreeTextFilter filter)
        {
            return string.Empty;
        }
    }
}
