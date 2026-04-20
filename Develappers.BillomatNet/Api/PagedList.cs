// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Develappers.BillomatNet.Api.Json;

namespace Develappers.BillomatNet.Api
{
    internal abstract class PagedList<T>
    {
        public abstract List<T> List { get; set; }

        [JsonPropertyName("@page")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int Page { get; set; }

        [JsonPropertyName("@per_page")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int PerPage { get; set; }

        [JsonPropertyName("@total")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int Total { get; set; }
    }
}
