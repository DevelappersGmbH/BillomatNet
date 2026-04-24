// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    /// <summary>
    /// Represents your account.
    /// </summary>
    internal class Account : Client
    {
        [JsonPropertyName("plan")]
        public string Plan { get; set; }

        [JsonPropertyName("quotas")]
        public QuotaWrapper Quotas { get; set; }
    }
}
