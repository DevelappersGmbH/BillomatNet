// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Develappers.BillomatNet.Api.Json;
using System.Text.Json.Serialization;

namespace Develappers.BillomatNet.Api
{
    internal class Settings
    {
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("update")]
        public DateTime Update { get; set; }
        [JsonPropertyName("bgcolor")]
        public string BgColor { get; set; }
        [JsonPropertyName("color1")]
        public string Color1 { get; set; }
        [JsonPropertyName("color2")]
        public string Color2 { get; set; }
        [JsonPropertyName("color3")]
        public string Color3 { get; set; }
        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
        [JsonPropertyName("locale")]
        public string Locale { get; set; }
        [JsonPropertyName("net_gross")]
        public string NetGross { get; set; }
        [JsonPropertyName("sepa_creditor_id")]
        public string SepaCreditorId { get; set; }
        [JsonPropertyName("number_range_mode")]
        public string NumberRangeMode { get; set; }
        [JsonPropertyName("article_number_pre")]
        public string ArticleNumberPre { get; set; }
        [JsonPropertyName("article_number_length")]
        public string ArticleNumberLength { get; set; }
        [JsonPropertyName("my_property")]
        public string MyProperty { get; set; }
        [JsonPropertyName("article_number_next")]
        public string ArticleNumberNext { get; set; }
        [JsonPropertyName("price_group2")]
        public string PriceGroup2 { get; set; }
        [JsonPropertyName("price_group3")]
        public string PriceGroup3 { get; set; }
        [JsonPropertyName("price_group4")]
        public string PriceGroup4 { get; set; }
        [JsonPropertyName("price_group5")]
        public string PriceGroup5 { get; set; }
        [JsonPropertyName("client_number_pre")]
        public string ClientNumberPre { get; set; }
        [JsonPropertyName("client_number_length")]
        public string ClientNumberLength { get; set; }
        [JsonPropertyName("client_number_next")]
        public string ClientNumberNext { get; set; }
        [JsonPropertyName("invoice_number_pre")]
        public string InvoiceNumberPre { get; set; }
        [JsonPropertyName("invoice_number_length")]
        public string InvoiceNumberLength { get; set; }
        [JsonPropertyName("invoice_number_next")]
        public string InvoiceNumberNext { get; set; }
        [JsonPropertyName("invoice_label")]
        public string InvoiceLabel { get; set; }
        [JsonPropertyName("invoice_intro")]
        public string InvoiceIntro { get; set; }
        [JsonPropertyName("invoice_note")]
        public string InvoiceNote { get; set; }
        [JsonPropertyName("inoice_filename")]
        public string InvoiceFilename { get; set; }
        [JsonPropertyName("due_days")]
        public string DueDays { get; set; }
        [JsonPropertyName("discount_rate")]
        public string DiscountRate { get; set; }
        [JsonPropertyName("discountd_days")]
        public string DiscountDays { get; set; }
        [JsonPropertyName("offer_number_pre")]
        public string OfferNumberPre { get; set; }
        [JsonPropertyName("offer_number_length")]
        public string OfferNumberLength { get; set; }
        [JsonPropertyName("offer_number_next")]
        public string OfferNumberNext { get; set; }
        [JsonPropertyName("offer_label")]
        public string OfferLabel { get; set; }
        [JsonPropertyName("offer_intro")]
        public string OfferIntro { get; set; }
        [JsonPropertyName("offer_note")]
        public string OfferNote { get; set; }
        [JsonPropertyName("offer_filename")]
        public string OfferFilename { get; set; }
        [JsonPropertyName("offer_validity_days")]
        public string OfferValidityDays { get; set; }
        [JsonPropertyName("confirmation_number_pre")]
        public string ConfirmationNumberPre { get; set; }
        [JsonPropertyName("confirmation_number_length")]
        public string ConfirmationNumberLength { get; set; }
        [JsonPropertyName("confirmation_number_next")]
        public string ConfirmationNumberNext { get; set; }
        [JsonPropertyName("confirmation_label")]
        public string ConfirmationLabel { get; set; }
        [JsonPropertyName("confirmation_intro")]
        public string ConfirmationIntro { get; set; }
        [JsonPropertyName("confirmation_note")]
        public string ConfirmationNote { get; set; }
        [JsonPropertyName("confirmation_filename")]
        public string ConfirmationFilename { get; set; }
        [JsonPropertyName("credit_number_pre")]
        public string CreditNumberPre { get; set; }
        [JsonPropertyName("credit_number_length")]
        public string CreditNumberLength { get; set; }
        [JsonPropertyName("credit_number_next")]
        public string CreditNumberNext { get; set; }
        [JsonPropertyName("credit_label")]
        public string CreditLabel { get; set; }
        [JsonPropertyName("credit_intro")]
        public string CreditIntro { get; set; }
        [JsonPropertyName("credit_note")]
        public string CreditNote { get; set; }
        [JsonPropertyName("credit_filename")]
        public string CreditFilename { get; set; }
        [JsonPropertyName("delivery_number_pre")]
        public string DeliveryNumberPre { get; set; }
        [JsonPropertyName("delivery_number_length")]
        public string DeliveryNumberLength { get; set; }
        [JsonPropertyName("delivery_number_next")]
        public string DeliveryNumberNext { get; set; }
        [JsonPropertyName("delivery_label")]
        public string DeliveryLabel { get; set; }
        [JsonPropertyName("delivery_intro")]
        public string DeliveryIntro { get; set; }
        [JsonPropertyName("delivery_note")]
        public string DeliveryNote { get; set; }
        [JsonPropertyName("delivery_filename")]
        public string DeliveryFilename { get; set; }
        [JsonPropertyName("reminder_filename")]
        public string ReminderFilename { get; set; }
        [JsonPropertyName("reminder_due_days")]
        public string ReminderDueDays { get; set; }
        [JsonPropertyName("letter_label")]
        public string LetterLabel { get; set; }
        [JsonPropertyName("letter_intro")]
        public string LetterIntro { get; set; }
        [JsonPropertyName("letter_filename")]
        public string LetterFilename { get; set; }
        [JsonPropertyName("template_engine")]
        public string TemplateEngine { get; set; }
        [JsonPropertyName("print_version")]
        public string PrintVersion { get; set; }
        [JsonPropertyName("default_email_sender")]
        public string DefaultEmailSender { get; set; }

        [JsonPropertyName("bcc_addresses")]
        [JsonConverter(typeof(CollectionConverter<BccAddressType>))]
        public List<BccAddressType> BccAddresses { get; set; }
    }
}
