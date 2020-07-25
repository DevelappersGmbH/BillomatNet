// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using System;
using System.Linq;
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
                SepaCreditorId = value.SepaCreditorId,
                NumberRangeMode = numberRangeMode,
                ArticleNumberPre = value.ArticleNumberPre,
                ArticleNumberLength = value.ArticleNumberLength.ToOptionalInt(),
                ArticleNumberNext = value.ArticleNumberNext.ToOptionalInt(),
                PriceGroup2 = value.PriceGroup2,
                PriceGroup3 = value.PriceGroup3,
                PriceGroup4 = value.PriceGroup4,
                PriceGroup5 = value.PriceGroup5,
                ClientNumberPre = value.ClientNumberPre,
                ClientNumberLength = value.ClientNumberLength.ToOptionalInt(),
                ClientNumberNext = value.ClientNumberNext.ToOptionalInt(),
                InvoiceNumberPre = value.InvoiceNumberPre,
                InvoiceNumberLength = value.InvoiceNumberLength.ToOptionalInt(),
                InvoiceNumberNext = value.InvoiceNumberNext.ToOptionalInt(),
                InvoiceLabel = value.InvoiceLabel,
                InvoiceIntro = value.InvoiceIntro,
                InvoiceNote = value.InvoiceNote,
                InvoiceFilename = value.InvoiceFilename,
                DueDays = value.DueDays.ToOptionalInt(),
                DiscountRate = value.DiscountRate.ToOptionalFloat(),
                DiscountDays = value.DiscountDays.ToOptionalInt(),
                OfferNumberPre = value.OfferNumberPre,
                OfferNumberLength = value.OfferNumberLength.ToOptionalInt(),
                OfferNumberNext = value.OfferNumberNext.ToOptionalInt(),
                OfferLabel = value.OfferLabel,
                OfferIntro = value.OfferIntro,
                OfferNote = value.OfferNote,
                OfferFilename = value.OfferFilename,
                OfferValidityDays = value.OfferValidityDays.ToOptionalInt(),
                ConfirmationNumberPre = value.ConfirmationNumberPre,
                ConfirmationNumberLength = value.ConfirmationNumberLength.ToOptionalInt(),
                ConfirmationNumberNext = value.ConfirmationNumberNext.ToOptionalInt(),
                ConfirmationLabel = value.ConfirmationLabel,
                ConfirmationIntro = value.ConfirmationIntro,
                ConfirmationNote = value.ConfirmationNote,
                ConfirmationFilename = value.ConfirmationFilename,
                CreditNumberPre = value.CreditNumberPre,
                CreditNumberLength = value.CreditNumberLength.ToOptionalInt(),
                CreditNumberNext = value.CreditNumberNext.ToOptionalInt(),
                CreditLabel = value.CreditLabel,
                CreditIntro = value.CreditIntro,
                CreditNote = value.CreditNote,
                CreditFilename = value.CreditFilename,
                DeliveryNumberPre = value.DeliveryNumberPre,
                DeliveryNumberLength = value.DeliveryNumberLength.ToOptionalInt(),
                DeliveryNumberNext = value.DeliveryNumberNext.ToOptionalInt(),
                DeliveryLabel = value.DeliveryLabel,
                DeliveryIntro = value.DeliveryIntro,
                DeliveryNote = value.DeliveryNote,
                DeliveryFilename = value.DeliveryFilename,
                ReminderFilename = value.ReminderFilename,
                ReminderDueDays = value.ReminderDueDays.ToOptionalInt(),
                LetterLabel = value.LetterLabel,
                LetterIntro = value.LetterIntro,
                LetterFilename = value.LetterFilename,
                TemplateEngine = value.TemplateEngine,
                PrintVersion = value.PrintVersion.ToOptionalInt(),
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
