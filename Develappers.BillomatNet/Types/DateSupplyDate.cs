// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents the supply date for an invoice
    /// </summary>
    public class DateSupplyDate : ISupplyDate
    {
        public DateTime? Date { get; set; }
    }
}
