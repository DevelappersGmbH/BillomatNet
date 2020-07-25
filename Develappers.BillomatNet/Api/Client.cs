// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Client
    {
        // TODO: see missing properties https://www.billomat.com/api/kunden/

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("client_number")]
        public string ClientNumber { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("archived")]
        public string Archived { get; set; }

        [JsonProperty("number_pre")]
        public string NumberPre { get; set; }

        [JsonProperty("number_length")]
        public string NumberLength { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("salutation")]
        public string Salutation { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("www")]
        public string Www { get; set; }

        [JsonProperty("tax_number")]
        public string TaxNumber { get; set; }

        [JsonProperty("vat_number")]
        public string VatNumber { get; set; }

        [JsonProperty("bank_account_owner")]
        public string BankAccountOwner { get; set; }

        [JsonProperty("bank_number")]
        public string BankNumber { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("bank_account_number")]
        public string BankAccountNumber { get; set; }

        [JsonProperty("bank_swift")]
        public string BankSwift { get; set; }

        [JsonProperty("bank_iban")]
        public string BankIban { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        [JsonProperty("net_gross")]
        public string NetGross { get; set; }
    }
}
