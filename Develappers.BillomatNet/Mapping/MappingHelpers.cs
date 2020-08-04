// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Mapping
{
    internal static class MappingHelpers
    {
        /// <summary>
        /// Checks the PropertyType enum and returns if checked or not.
        /// </summary>
        /// <param name="type">The enum</param>
        /// <param name="value">The string</param>
        /// <returns>The object boolean</returns>
        public static object ParsePropertyValue(PropertyType type, string value)
        {
            if (type == PropertyType.Checkbox)
            {
                return value != "0";
            }

            return value;
        }

        public static string ParsePropertyValue(PropertyType type, object value)
        {
            if (type == PropertyType.Checkbox)
            {
                try
                {
                    var b = (bool)value;
                    if (b)
                    {
                        return "1";
                    }
                    return "0";
                }
                catch (InvalidCastException)
                {

                    return "0";
                }
            }
            return (string)value;
        }

        public static string BoolToString(this bool value)
        {
            if (value)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        internal static TagCloudItem ToDomain(this Api.TagCloudItem value)
        {
            if (value == null)
            {
                return null;
            }

            return new TagCloudItem
            {
                Id = value.Id,
                Count = value.Count,
                Name = value.Name
            };
        }

        internal static NetGrossType ToNetGrossType(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            switch (value.ToLowerInvariant())
            {
                case "net":
                    return NetGrossType.Net;
                case "gross":
                    return NetGrossType.Gross;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        internal static NetGrossSettingsType ToNetGrossSettingsType(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            switch (value.ToLowerInvariant())
            {
                case "net":
                    return NetGrossSettingsType.Net;
                case "gross":
                    return NetGrossSettingsType.Gross;
                default:
                    return NetGrossSettingsType.Settings;
            };
        }

        internal static string ToApiValue(this NetGrossSettingsType value)
        {
            return value.ToString().ToUpperInvariant();
        }

        internal static string ToApiValue(this NetGrossType value)
        {
            return value.ToString().ToUpperInvariant();
        }

        internal static string ToInvariantString(this float? value)
        {
            return value?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the InvoiceStatus enum to string
        /// </summary>
        /// <param name="value">The enum</param>
        /// <returns>
        /// The string or an exception if the value doesn't match any case
        /// </returns>
        internal static string ToApiValue(this InvoiceStatus value)
        {
            switch (value)
            {
                case InvoiceStatus.Draft:
                    return "DRAFT";
                case InvoiceStatus.Open:
                    return "OPEN";
                case InvoiceStatus.Paid:
                    return "PAID";
                case InvoiceStatus.Overdue:
                    return "OVERDUE";
                case InvoiceStatus.Canceled:
                    return "CANCELED";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        /// <summary>
        /// Converts the string to PropertyType enum
        /// </summary>
        /// <param name="value">The string</param>
        /// <returns>The enum</returns>
        internal static PropertyType ToPropertyType(this string value)
        {
            switch (value)
            {
                case "CHECKBOX":
                    return PropertyType.Checkbox;
                case "TEXTAREA":
                    return PropertyType.Textarea;
                default:
                    return PropertyType.Textfield;
            }
        }

        internal static string ToApiValue(this PropertyType value)
        {
            switch (value)
            {
                case PropertyType.Textarea:
                    return "TEXTAREA";
                case PropertyType.Checkbox:
                    return "CHECKBOX";
                default:
                    return "TEXTFIELD";
            }
        }

        internal static CommentType ToCommentType(this string value)
        {
            switch (value)
            {
                case "COMMENT":
                    return CommentType.Comment;
                case "COPY":
                    return CommentType.Copy;
                case "CREATE_FROM_OFFER":
                    return CommentType.CreateFromOffer;
                case "CREATE_FROM_INVOICE":
                    return CommentType.CreateFromInvoice;
                case "CREATE_FROM_RECURRING":
                    return CommentType.CreateFromRecurring;
                case "STATUS":
                    return CommentType.Status;
                case "PAYMENT":
                    return CommentType.Payment;
                case "PAYMENT_ERROR":
                    return CommentType.PaymentError;
                case "DELETE_PAYMENT":
                    return CommentType.DeletePayment;
                case "MAIL":
                    return CommentType.Mail;
                case "LETTER":
                    return CommentType.Letter;
                case "FAX":
                    return CommentType.Fax;
                case "SIGN":
                    return CommentType.Sign;
                case "SIGN_MAIL":
                    return CommentType.SignMail;
                case "CANCEL":
                    return CommentType.Cancel;
                case "ERROR_MAIL":
                    return CommentType.ErrorMail;
                case "CREATE_CREDIT_NOTE":
                    return CommentType.CreateCreditNote;
                case "REMINDER_CREATE":
                    return CommentType.ReminderCreate;
                case "REMINDER_STATUS":
                    return CommentType.ReminderStatus;
                case "REMINDER_MAIL":
                    return CommentType.ReminderMail;
                case "REMINDER_ERROR_MAIL":
                    return CommentType.ReminderErrorMail;
                case "REMINDER_LETTER":
                    return CommentType.ReminderLetter;
                case "REMINDER_FAX":
                    return CommentType.ReminderFax;
                case "REMINDER_SIGN":
                    return CommentType.ReminderSign;
                case "REMINDER_SIGN_MAIL":
                    return CommentType.ReminderSignMail;
                case "REMINDER_CANCEL":
                    return CommentType.ReminderCancel;
                case "REMINDER_DELETE":
                    return CommentType.ReminderDelete;
                default:
                    return CommentType.Create;
            }
        }

        internal static string ToApiValue(this CommentType value)
        {
            switch (value)
            {
                case CommentType.Create:
                    return "CREATE";
                case CommentType.Copy:
                    return "COPY";
                case CommentType.CreateFromOffer:
                    return "CREATE_FROM_OFFER";
                case CommentType.CreateFromInvoice:
                    return "CREATE_FROM_INVOICE";
                case CommentType.CreateFromRecurring:
                    return "CREATE_FROM_REURRING";
                case CommentType.Status:
                    return "STATUS";
                case CommentType.Payment:
                    return "PAYMENT";
                case CommentType.PaymentError:
                    return "PAYMENT_ERROR";
                case CommentType.DeletePayment:
                    return "DELETE_PAYMENT";
                case CommentType.Mail:
                    return "MAIL";
                case CommentType.Letter:
                    return "LETTER";
                case CommentType.Fax:
                    return "FAX";
                case CommentType.Sign:
                    return "SIGN";
                case CommentType.SignMail:
                    return "SIGN_MAIL";
                case CommentType.Cancel:
                    return "CANCEL";
                case CommentType.ErrorMail:
                    return "ERROR_MAIL";
                case CommentType.CreateCreditNote:
                    return "CREATE_CREDIT_NOTE";
                case CommentType.ReminderCreate:
                    return "REMINDER_CREATE";
                case CommentType.ReminderStatus:
                    return "REMINDER_STATUS";
                case CommentType.ReminderMail:
                    return "REMINDER_MAIL";
                case CommentType.ReminderErrorMail:
                    return "REMINDER_ERROR_MAIL";
                case CommentType.ReminderLetter:
                    return "REMINDER_LETTER";
                case CommentType.ReminderFax:
                    return "REMINDER_FAX";
                case CommentType.ReminderSign:
                    return "REMINDER_SIGN";
                case CommentType.ReminderSignMail:
                    return "REMINDER_SIGN_MAIL";
                case CommentType.ReminderCancel:
                    return "REMINDER_CANCEL";
                case CommentType.ReminderDelete:
                    return "REMINDER_DELETE";
                default:
                    return "COMMENT";
             }
        }
        internal static PaymentType ToPaymentType(this string value)
        {
            switch (value)
            {
                case "CREDIT_NOTE":
                    return PaymentType.CreditNote;
                case "BANK_CARD":
                    return PaymentType.BankCard;
                case "BANK_TRANSFER":
                    return PaymentType.BankTransfer;
                case "DEBIT":
                    return PaymentType.Debit;
                case "CASH":
                    return PaymentType.Cash;
                case "CHECK":
                    return PaymentType.Check;
                case "PAYPAL":
                    return PaymentType.PayPal;
                case "CREDIT_CARD":
                    return PaymentType.CreditCard;
                case "COUPON":
                    return PaymentType.Coupon;
                case "MISC":
                    return PaymentType.Misc;
                default:
                    return PaymentType.InvoiceCorrection;
            }
        }

        /// <summary>
        /// Converts the PaymentType enum to string
        /// </summary>
        /// <param name="value">The enum</param>
        /// <returns>
        /// The string or an exception if the value doesn't match any case
        /// </returns>
        internal static string ToApiValue(this PaymentType value)
        {
            switch (value)
            {
                case PaymentType.InvoiceCorrection:
                    return "INVOICE_CORRETCITONS";
                case PaymentType.CreditNote:
                    return "CREDIT_NOTE";
                case PaymentType.BankCard:
                    return "BANK_CARD";
                case PaymentType.BankTransfer:
                    return "BANK_TRANSFER";
                case PaymentType.Debit:
                    return "DEBIT";
                case PaymentType.Cash:
                    return "CASH";
                case PaymentType.Check:
                    return "CHECK";
                case PaymentType.PayPal:
                    return "PAYPAL";
                case PaymentType.CreditCard:
                    return "CREDIT_CARD";
                case PaymentType.Coupon:
                    return "COUPON";
                case PaymentType.Misc:
                    return "MISC";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        internal static TaxRuleType ToTaxRuleType(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            switch (value.ToLowerInvariant())
            {
                case "tax":
                    return TaxRuleType.Tax;
                case "no_tax":
                    return TaxRuleType.NoTax;
                default:
                    return TaxRuleType.Country;
            }
        }

        internal static string ToApiValue(this TaxRuleType value)
        {
            switch (value)
            {
                case TaxRuleType.Tax:
                    return value.ToString().ToUpperInvariant();
                case TaxRuleType.NoTax:
                    return "NO_TAX";
                default:
                    return value.ToString().ToUpperInvariant();
            }
        }

        internal static DiscountType ToDiscountType(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            switch (value.ToLowerInvariant())
            {
                case "absolute":
                    return DiscountType.Absolute;
                case "relative":
                    return DiscountType.Relative;
                default:
                    return DiscountType.Settings;
            }
        }

        internal static string ToApiValue(this DiscountType value)
        {
            return value.ToString().ToUpperInvariant();
        }
    }
}
