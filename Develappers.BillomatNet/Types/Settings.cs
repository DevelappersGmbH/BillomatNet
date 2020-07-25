using System;
using System.Collections.Generic;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents the settings.
    /// </summary>
    public class Settings
    {
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        public string BgColor { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string CurrencyCode { get; set; }
        public string Locale { get; set; }
        public NetGrossType NetGross { get; set; }
        public string SepaCreditorId { get; set; }
        public NumberRangeModeType NumberRangeMode { get; set; }
        public string ArticleNumberPre { get; set; }
        public int? ArticleNumberLength { get; set; }
        public int? ArticleNumberNext { get; set; }
        public string PriceGroup2 { get; set; }
        public string PriceGroup3 { get; set; }
        public string PriceGroup4 { get; set; }
        public string PriceGroup5 { get; set; }
        public string ClientNumberPre { get; set; }
        public int? ClientNumberLength { get; set; }
        public int? ClientNumberNext { get; set; }
        public string InvoiceNumberPre { get; set; }
        public int? InvoiceNumberLength { get; set; }
        public int? InvoiceNumberNext { get; set; }
        public string InvoiceLabel { get; set; }
        public string InvoiceIntro { get; set; }
        public string InvoiceNote { get; set; }
        public string InvoiceFilename { get; set; }
        public int? DueDays { get; set; }
        public float? DiscountRate { get; set; }
        public int? DiscountDays { get; set; }
        public string OfferNumberPre { get; set; }
        public int? OfferNumberLength { get; set; }
        public int? OfferNumberNext { get; set; }
        public string OfferLabel { get; set; }
        public string OfferIntro { get; set; }
        public string OfferNote { get; set; }
        public string OfferFilename { get; set; }
        public int? OfferValidityDays { get; set; }
        public string ConfirmationNumberPre { get; set; }
        public int? ConfirmationNumberLength { get; set; }
        public int? ConfirmationNumberNext { get; set; }
        public string ConfirmationLabel { get; set; }
        public string ConfirmationIntro { get; set; }
        public string ConfirmationNote { get; set; }
        public string ConfirmationFilename { get; set; }
        public string CreditNumberPre { get; set; }
        public int? CreditNumberLength { get; set; }
        public int? CreditNumberNext { get; set; }
        public string CreditLabel { get; set; }
        public string CreditIntro { get; set; }
        public string CreditNote { get; set; }
        public string CreditFilename { get; set; }
        public string DeliveryNumberPre { get; set; }
        public int? DeliveryNumberLength { get; set; }
        public int? DeliveryNumberNext { get; set; }
        public string DeliveryLabel { get; set; }
        public string DeliveryIntro { get; set; }
        public string DeliveryNote { get; set; }
        public string DeliveryFilename { get; set; }
        public string ReminderFilename { get; set; }
        public int? ReminderDueDays { get; set; }
        public string LetterLabel { get; set; }
        public string LetterIntro { get; set; }
        public string LetterFilename { get; set; }
        public string TemplateEngine { get; set; }
        public int? PrintVersion { get; set; }
        public string DefaultEmailSender { get; set; }
        public List<string> BccAddresses { get; set; }
    }
}
