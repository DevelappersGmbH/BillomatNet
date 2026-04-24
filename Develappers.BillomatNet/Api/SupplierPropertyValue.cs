// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class SupplierPropertyValue
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("supplier_id")]
        public string SupplierId { get; set; }
        [JsonPropertyName("supplier_property_id")]
        public string SupplierPropertyId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
