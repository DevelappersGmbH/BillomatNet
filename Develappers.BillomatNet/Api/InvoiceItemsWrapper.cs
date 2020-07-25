// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceItemsWrapper
    {
        [JsonProperty("invoice_items")]
        [JsonConverter(typeof(CollectionConverter<InvoiceItem>))]
        public List<InvoiceItem> List { get; set; }
    }
}
