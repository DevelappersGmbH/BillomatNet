// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Develappers.BillomatNet.Api.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceList : PagedList<Invoice>
    {
        [JsonPropertyName("invoice")]
        [JsonConverter(typeof(CollectionConverter<Invoice>))]
        public override List<Invoice> List { get; set; }
    }
}
