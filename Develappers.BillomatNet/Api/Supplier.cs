// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Supplier
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("created")]
        public string Created { get; set; }
        [JsonPropertyName("updated")]
        public string Updated { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("salutation")]
        public string Salutation { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("zip")]
        public string Zip { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
        [JsonPropertyName("is_eu_country")]
        public string IsEuCountry { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("fax")]
        public string Fax { get; set; }
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("www")]
        public string Www { get; set; }
        [JsonPropertyName("tax_number")]
        public string TaxNumber { get; set; }
        [JsonPropertyName("vat_number")]
        public string VatNumber { get; set; }
        [JsonPropertyName("bank_account_owner")]
        public string BankAccountOwner { get; set; }
        [JsonPropertyName("bank_number")]
        public string BankNumber { get; set; }
        [JsonPropertyName("bank_name")]
        public string BankName { get; set; }
        [JsonPropertyName("bank_account_number")]
        public string BankAccountNumber { get; set; }
        [JsonPropertyName("bank_swift")]
        public string BankSwift { get; set; }
        [JsonPropertyName("bank_iban")]
        public string BankIban { get; set; }
        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
        [JsonPropertyName("locale")]
        public string Locale { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
        [JsonPropertyName("client_number")]
        public string ClientNumber { get; set; }
        [JsonPropertyName("creditor_account_number")]
        public string CreditorAccountNumber { get; set; }
        [JsonPropertyName("creditor_identifier")]
        public string CreditorIdentifier { get; set; }
        [JsonPropertyName("costs_gross")]
        public string CostsGross { get; set; }
        [JsonPropertyName("costs_net")]
        public string CostsNet { get; set; }
        [JsonPropertyName("supplier-property-values")]
        public SupplierPropertyValuesWrapper SupplierPropertyValues { get; set; }
    }
}
