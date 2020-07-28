// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Develappers.BillomatNet.Types;
using Microsoft.Win32.SafeHandles;

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

        internal static string ToApiValue(this NetGrossType value)
        {
            return value.ToString().ToUpperInvariant();
        }

        internal static string ToInvariantString(this float? value)
        {
            return value?.ToString(CultureInfo.InvariantCulture);
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
                case PaymentType.Cash:
                    return "CASH";
                case PaymentType.BankTransfer:
                    return "BANK_TRANSFER";
                case PaymentType.PayPal:
                    return "PAYPAL";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
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
                case "Comment":
                    return CommentType.Comment;
                case "Create":
                    return CommentType.Create;
                case "Copy":
                    return CommentType.Copy;
                case "CreateFromOffer":
                    return CommentType.CreateFromOffer;
                case "CreateFromInvoice":
                    return CommentType.CreateFromInvoice;
                case "CreateFromRecurring":
                    return CommentType.CreateFromRecurring;
                case "Status":
                    return CommentType.Status;
                case "Payment":
                    return CommentType.Payment;
                case "PaymentError":
                    return CommentType.PaymentError;
                case "DeletePayment":
                    return CommentType.DeletePayment;
                case "Mail":
                    return CommentType.Mail;
                case "Letter":
                    return CommentType.Letter;
                case "Fax":
                    return CommentType.Fax;
                case "Sign":
                    return CommentType.Sign;
                case "SignMail":
                    return CommentType.SignMail;
                case "Cancel":
                    return CommentType.Cancel;
                case "ErrorMail":
                    return CommentType.ErrorMail;
                case "CreateCreditNote":
                    return CommentType.CreateCreditNote;
                case "ReminderCreate":
                    return CommentType.ReminderCreate;
                case "ReminderStatus":
                    return CommentType.ReminderStatus;
                case "ReminderMail":
                    return CommentType.ReminderMail;
                case "ReminderErrorMail":
                    return CommentType.ReminderErrorMail;
                case "RemidnerLetter":
                    return CommentType.RemidnerLetter;
                case "ReminderFax":
                    return CommentType.ReminderFax;
                case "ReminderSign":
                    return CommentType.ReminderSign;
                case "ReminderSignMail":
                    return CommentType.ReminderSignMail;
                case "ReminderCancel":
                    return CommentType.ReminderCancel;
                default:
                    return CommentType.ReminderDelete;
            }
        }
    }
}
