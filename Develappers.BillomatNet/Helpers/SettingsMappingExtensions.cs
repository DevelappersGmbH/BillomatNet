using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using System;
using System.Linq;
using System.Security;
using Settings = Develappers.BillomatNet.Types.Settings;

namespace Develappers.BillomatNet.Helpers
{
    internal static class SettingsMappingExtensions
    {
        internal static Settings ToDomain(this SettingsWrapper value)
        {
            return value?.Settings.ToDomain();
        }

        private static Settings ToDomain(this Api.Settings value)
        {
            if (value == null)
            {
                return null;
            }

            NetGrossType netGrossType;
            switch (value.NetGross.ToLowerInvariant())
            {
                case "net":
                    netGrossType = NetGrossType.Net;
                    break;
                case "gross":
                    netGrossType = NetGrossType.Gross;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            NumberRangeModeType numberRangeMode;
            switch (value.NumberRangeMode.ToLowerInvariant())
            {
                case "ignore_prefix":
                    numberRangeMode = NumberRangeModeType.IgnorePrefix;
                    break;
                case "consider_prefix":
                    numberRangeMode = NumberRangeModeType.ConsiderPrefix;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new Settings
            {
                Created = value.Created,
                Update = value.Update,
                BgColor = value.BgColor,
                Color1 = value.Color1,
                Color2 = value.Color2,
                Color3 = value.Color3,
                CurrencyCode = value.CurrencyCode,
                Locale = value.Locale,
                NetGross = netGrossType,
                SepaCreditorId = int.Parse(value.SepaCreditorId),
                NumberRangeMode = numberRangeMode,
                ArticleNumberPre = value.ArticleNumberPre,
                ArticleNumberLength = int.Parse(value.ArticleNumberLength),
                ArticleNumberNext = int.Parse(value.ArticleNumberNext),
                PriceGroup2 = value.PriceGroup2,
                PriceGroup3 = value.PriceGroup3,
                PriceGroup4 = value.PriceGroup4,
                PriceGroup5 = value.PriceGroup5,
                ClientNumberPre = value.ClientNumberPre,
                ClientNumberLength = int.Parse(value.ClientNumberLength),
                ClientNumberNext = int.Parse(value.ClientNumberNext),
                InvoiceNumberPre = value.InvoiceNumberPre,
                InvoiceNumberLength = int.Parse(value.InvoiceNumberLength),
                InvoiceNumberNext = int.Parse(value.InvoiceNumberNext),
                InvoiceLabel = value.InvoiceNumberNext,
                InvoiceIntro = value.InvoiceNumberNext,
                InvoiceNote = value.InvoiceNumberNext,
                InvoiceFilename = value.InvoiceNumberNext,
                DueDays = int.Parse(value.DueDays),
                DiscountRate = int.Parse(value.DiscountDays),
                DiscountDays = int.Parse(value.DiscountDays),
                OfferNumberPre = value.OfferNumberPre,
                OfferNumberLength = int.Parse(value.OfferNumberLength),
                OfferNumberNext = int.Parse(value.OfferNumberNext),
                OfferLabel = value.OfferLabel,
                OfferIntro = value.OfferIntro,
                OfferNote = value.OfferNote,
                OfferFilename = value.OfferFilename,
                OfferValidityDays = int.Parse(value.OfferValidityDays),
                ConfirmationNumberPre = value.ConfirmationNumberPre,
                ConfirmationNumberLength = int.Parse(value.ConfirmationNumberLength),
                ConfirmationNumberNext = int.Parse(value.ConfirmationNumberNext),
                ConfirmationLabel = value.ConfirmationLabel,
                ConfirmationIntro = value.ConfirmationIntro,
                ConfirmationNote = value.ConfirmationNote,
                ConfirmationFilename = value.ConfirmationFilename,
                CreditNumberPre = value.CreditNumberPre,
                CreditNumberLength = int.Parse(value.CreditNumberLength),
                CreditNumberNext = int.Parse(value.CreditNumberNext),
                CreditLabel = value.CreditLabel,
                CreditIntro = value.CreditIntro,
                CreditNote = value.CreditNote,
                CreditFilename = value.CreditFilename,
                DeliveryNumberPre = value.DeliveryNumberPre,
                DeliveryNumberLength = int.Parse(value.DeliveryNumberLength),
                DeliveryNumberNext = int.Parse(value.DeliveryNumberNext),
                DeliveryLabel = value.DeliveryLabel,
                DeliveryIntro = value.DeliveryIntro,
                DeliveryNote = value.DeliveryNote,
                DeliveryFilename = value.DeliveryFilename,
                ReminderFilename = value.ReminderFilename,
                ReminderDueDays = int.Parse(value.ReminderDueDays),
                LetterLabel = value.LetterLabel,
                LetterIntro = value.LetterIntro,
                LetterFilename = value.LetterFilename,
                TemplateEngine = value.TemplateEngine,
                PrintVersion = int.Parse(value.PrintVersion),
                DefaultEmailSender = value.DefaultEmailSender,
                BccAddresses = value.BccAddresses.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static Types.BccAddressType ToDomain(this Api.BccAddressType value)
        {
            return new Types.BccAddressType { BccAddress = value.BccAddress };
        }
    }
}
