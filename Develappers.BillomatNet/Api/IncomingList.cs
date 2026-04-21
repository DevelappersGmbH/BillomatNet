// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Develappers.BillomatNet.Api.Json;

namespace Develappers.BillomatNet.Api
{
    internal class IncomingList : PagedList<Incoming>
    {
        [JsonPropertyName("incoming")]
        [JsonConverter(typeof(CollectionConverter<Incoming>))]
        public override List<Incoming> List { get; set; }
    }
}
