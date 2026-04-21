// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class ClientPropertyListWrapper : PagedListWrapper<ClientPropertyList>
    {
        [JsonPropertyName("client-property-values")]
        public override ClientPropertyList Item { get; set; }
    }
}
