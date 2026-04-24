// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Client
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("archived")]
        public string Archived { get; set; }

        [JsonPropertyName("client_number")]
        public string ClientNumber { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("number_pre")]
        public string NumberPre { get; set; }

        [JsonPropertyName("number_length")]
        public string NumberLength { get; set; }

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

        [JsonPropertyName("enable_customerportal")]
        public string EnableCustomerportal { get; set; }

        [JsonPropertyName("customerportal_url")]
        public string CustomerportalUrl { get; set; }

        [JsonPropertyName("sepa_mandate")]
        public string SepaMandate { get; set; }

        [JsonPropertyName("sepa_mandate_date")]
        public string SepaMandateDate { get; set; }

        [JsonPropertyName("tax_rule")]
        public string TaxRule { get; set; }

        [JsonPropertyName("net_gross")]
        public string NetGross { get; set; }

        [JsonPropertyName("default_payment_types")]
        public string DefaultPaymentTypes { get; set; }

        [JsonPropertyName("reduction")]
        public string Reduction { get; set; }

        [JsonPropertyName("discount_rate_type")]
        public string DiscountRateType { get; set; }

        [JsonPropertyName("discount_rate")]
        public string DiscountRate { get; set; }

        [JsonPropertyName("discount_days_type")]
        public string DiscountDaysType { get; set; }

        [JsonPropertyName("discount_days")]
        public string DicountDays { get; set; }

        [JsonPropertyName("due_days_type")]
        public string DueDaysType { get; set; }

        [JsonPropertyName("due_days")]
        public string DueDays { get; set; }

        [JsonPropertyName("reminder_due_days_type")]
        public string ReminderDueDaysType { get; set; }

        [JsonPropertyName("reminder_due_days")]
        public string ReminderDueDays { get; set; }

        [JsonPropertyName("offer_validity_days_type")]
        public string OfferValidityDaysType { get; set; }

        [JsonPropertyName("offer_validity_days")]
        public string OfferValidityDays { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("price_group")]
        public string PriceGroup { get; set; }

        [JsonPropertyName("debitor_account_number")]
        public string DebitorAccountNumber { get; set; }

        [JsonPropertyName("dunning_run")]
        public string DunningRun { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("revenue_gross")]
        public string RevenueGross { get; set; }

        [JsonPropertyName("revenue_net")]
        public string RevenueNet { get; set; }
    }
}
