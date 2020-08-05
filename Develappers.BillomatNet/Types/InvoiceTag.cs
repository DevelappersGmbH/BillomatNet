// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents a invoice tag.
    /// </summary>
    public class InvoiceTag
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Name { get; set; }
    }
}
