using Develappers.BillomatNet.Api.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Develappers.BillomatNet.Api
{
    internal class Settings
    {
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("update")]
        public DateTime Update { get; set; }
        [JsonProperty("bgcolor")]
        public string BgColor { get; set; }
        [JsonProperty("color1")]
        public string Color1 { get; set; }
        [JsonProperty("color2")]
        public string Color2 { get; set; }
        [JsonProperty("color3")]
        public string Color3 { get; set; }
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
        [JsonProperty("net_gross")]
        public string NetGross { get; set; }
        [JsonProperty("sepa_creditor_id")]
        public string SepaCreditorId { get; set; }
        [JsonProperty("number_range_mode")]
        public string NumberRangeMode { get; set; }
        [JsonProperty("article_number_pre")]
        public string ArticleNumberPre { get; set; }
        [JsonProperty("article_number_length")]
        public string ArticleNumberLength { get; set; }
        [JsonProperty("my_property")]
        public string MyProperty { get; set; }
        [JsonProperty("article_number_next")]
        public string ArticleNumberNext { get; set; }
        [JsonProperty("price_group2")]
        public string PriceGroup2 { get; set; }
        [JsonProperty("price_group3")]
        public string PriceGroup3 { get; set; }
        [JsonProperty("price_group4")]
        public string PriceGroup4 { get; set; }
        [JsonProperty("price_group5")]
        public string PriceGroup5 { get; set; }
        [JsonProperty("client_number_pre")]
        public string ClientNumberPre { get; set; }
        [JsonProperty("client_number_length")]
        public string ClientNumberLength { get; set; }
        [JsonProperty("client_number_next")]
        public string ClientNumberNext { get; set; }
        [JsonProperty("invoice_number_pre")]
        public string InvoiceNumberPre { get; set; }
        [JsonProperty("invoice_number_length")]
        public string InvoiceNumberLength { get; set; }
        [JsonProperty("invoice_number_next")]
        public string InvoiceNumberNext { get; set; }
        [JsonProperty("invoice_label")]
        public string InvoiceLabel { get; set; }
        [JsonProperty("invoice_intro")]
        public string InvoiceIntro { get; set; }
        [JsonProperty("invoice_note")]
        public string InvoiceNote { get; set; }
        [JsonProperty("inoice_filename")]
        public string InvoiceFilename { get; set; }
        [JsonProperty("due_days")]
        public string DueDays { get; set; }
        [JsonProperty("discount_rate")]
        public string DiscountRate { get; set; }
        [JsonProperty("discountd_days")]
        public string DiscountDays { get; set; }
        [JsonProperty("offer_number_pre")]
        public string OfferNumberPre { get; set; }
        [JsonProperty("offer_number_length")]
        public string OfferNumberLength { get; set; }
        [JsonProperty("offer_number_next")]
        public string OfferNumberNext { get; set; }
        [JsonProperty("offer_label")]
        public string OfferLabel { get; set; }
        [JsonProperty("offer_intro")]
        public string OfferIntro { get; set; }
        [JsonProperty("offer_note")]
        public string OfferNote { get; set; }
        [JsonProperty("offer_filename")]
        public string OfferFilename { get; set; }
        [JsonProperty("offer_validity_days")]
        public string OfferValidityDays { get; set; }
        [JsonProperty("confirmation_number_pre")]
        public string ConfirmationNumberPre { get; set; }
        [JsonProperty("confirmation_number_length")]
        public string ConfirmationNumberLength { get; set; }
        [JsonProperty("confirmation_number_next")]
        public string ConfirmationNumberNext { get; set; }
        [JsonProperty("confirmation_label")]
        public string ConfirmationLabel { get; set; }
        [JsonProperty("confirmation_intro")]
        public string ConfirmationIntro { get; set; }
        [JsonProperty("confirmation_note")]
        public string ConfirmationNote { get; set; }
        [JsonProperty("confirmation_filename")]
        public string ConfirmationFilename { get; set; }
        [JsonProperty("credit_number_pre")]
        public string CreditNumberPre { get; set; }
        [JsonProperty("credit_number_length")]
        public string CreditNumberLength { get; set; }
        [JsonProperty("credit_number_next")]
        public string CreditNumberNext { get; set; }
        [JsonProperty("credit_label")]
        public string CreditLabel { get; set; }
        [JsonProperty("credit_intro")]
        public string CreditIntro { get; set; }
        [JsonProperty("credit_note")]
        public string CreditNote { get; set; }
        [JsonProperty("credit_filename")]
        public string CreditFilename { get; set; }
        [JsonProperty("delivery_number_pre")]
        public string DeliveryNumberPre { get; set; }
        [JsonProperty("delivery_number_length")]
        public string DeliveryNumberLength { get; set; }
        [JsonProperty("delivery_number_next")]
        public string DeliveryNumberNext { get; set; }
        [JsonProperty("delivery_label")]
        public string DeliveryLabel { get; set; }
        [JsonProperty("delivery_intro")]
        public string DeliveryIntro { get; set; }
        [JsonProperty("delivery_note")]
        public string DeliveryNote { get; set; }
        [JsonProperty("delivery_filename")]
        public string DeliveryFilename { get; set; }
        [JsonProperty("reminder_filename")]
        public string ReminderFilename { get; set; }
        [JsonProperty("reminder_due_days")]
        public string ReminderDueDays { get; set; }
        [JsonProperty("letter_label")]
        public string LetterLabel { get; set; }
        [JsonProperty("letter_intro")]
        public string LetterIntro { get; set; }
        [JsonProperty("letter_filename")]
        public string LetterFilename { get; set; }
        [JsonProperty("template_engine")]
        public string TemplateEngine { get; set; }
        [JsonProperty("print_version")]
        public string PrintVersion { get; set; }
        [JsonProperty("default_email_sender")]
        public string DefaultEmailSender { get; set; }

        [JsonProperty("bcc_addresses")]
        [JsonConverter(typeof(CollectionConverter<BccAddressType>))]
        public List<BccAddressType> BccAddresses { get; set; }
    }
}
