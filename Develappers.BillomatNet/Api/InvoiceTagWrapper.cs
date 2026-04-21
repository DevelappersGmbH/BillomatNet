// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceTagWrapper
    {
        [JsonPropertyName("invoice-tag")]
        public InvoiceTag InvoiceTag { get; set; }
    }
}
