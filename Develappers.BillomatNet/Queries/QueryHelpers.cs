// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Queries
{
    internal static class QueryHelpers
    {
        internal static string DateTimeToDateString (this DateTime? value)
        {
            return string.Join("-", value.Value.Year, value.Value.Month, value.Value.Day);
        }
    }
}
