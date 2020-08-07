// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Newtonsoft.Json;

namespace Develappers.BillomatNet.Api
{
    internal class Supplier
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("salutation")]
        public string Salutation { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("zip")]
        public string Zip { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("is_eu_country")]
        public string IsEuCountry { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("fax")]
        public string Fax { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
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
        [JsonProperty("locale")]
        public string Locale { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("client_number")]
        public string ClientNumber { get; set; }
        [JsonProperty("creditor_account_number")]
        public string CreditorAccountNumber { get; set; }
        [JsonProperty("creditor_identifier")]
        public string CreditorIdentifier { get; set; }
        [JsonProperty("costs_gross")]
        public string CostsGross { get; set; }
        [JsonProperty("costs_net")]
        public string CostsNet { get; set; }
        [JsonProperty("supplier-property-values")]
        public SupplierPropertyValuesWrapper SupplierPropertyValues { get; set; }
    }
}
