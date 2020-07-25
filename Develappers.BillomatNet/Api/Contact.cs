// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Contact
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("www")]
        public string Www { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zip")]
        public string ZipCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("salutation")]
        public string Salutation { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
    }
}
