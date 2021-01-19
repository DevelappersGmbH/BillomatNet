// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class InboxDocumentQueryStringBuilder : QueryStringBuilder<InboxDocument, Api.InboxDocument, InboxDocumentFilter>
    {
        protected internal override string GetFilterStringFor(InboxDocumentFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();

            return string.Join("&", filters);
        }
    }
}
