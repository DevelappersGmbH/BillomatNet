// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Develappers.BillomatNet.Types;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceTagWrapper
    {
        [JsonProperty("invoice-tag")]
        public InvoiceTag InvoiceTag { get; set; }
    }
}
