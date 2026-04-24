// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Develappers.BillomatNet.Api.Json;

namespace Develappers.BillomatNet.Api
{
    internal class InvoiceTaxWrapper
    {
        [JsonPropertyName("tax")]
        [JsonConverter(typeof(CollectionConverter<InvoiceTax>))]
        public List<InvoiceTax> List { get; set; }
    }
}
