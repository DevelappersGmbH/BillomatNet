// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Types;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Helpers
{
    internal static class CommonMappingExtensions
    {
        /// <summary>
        /// Converts a string to a nullable integer.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable integer or null if not found</returns>
        internal static int? ToOptionalInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return int.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a string to a nullable float.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable float or null if not found</returns>
        internal static float? ToOptionalFloat(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return float.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a string to a nullable DateTime.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The nullable DateTime or null if not found</returns>
        internal static DateTime? ToOptionalDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a nullable DateTime to a string.
        /// </summary>
        /// <param name="value">The nullable DateTime which gets converted.</param>
        /// <returns>The string.</returns>
        internal static string ToApiDate (this DateTime? value)
        {
            return value?.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Converts a DateTime to a string.
        /// </summary>
        /// <param name="value">The DateTime which gets converted.</param>
        /// <returns>The string.</returns>
        internal static string ToApiDate(this DateTime value)
        {
            return ((DateTime?)value).ToApiDate();
        }

        /// <summary>
        /// Converts a string to a boolean.
        /// </summary>
        /// <param name="value">The string which gets converted.</param>
        /// <returns>The boolean.</returns>
        internal static bool ToBoolean(this string value)
        {
            return value == "1";
        }

        /// <summary>
        /// Converts a string to a list of strings.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The the list of strings</returns>
        internal static List<string> ToStringList(this string value)
        {
            if (value == null)
            {
                return new List<string>();
            }

            return value.Split(',').Select(x => x.Trim()).ToList();
        }

        /// <summary>
        /// Converts a string to a list of integer.
        /// </summary>
        /// <param name="value">The string which gets converted</param>
        /// <returns>The the list of integer</returns>
        internal static List<int> ToIntList(this string value)
        {
            if (value == null)
            {
                return new List<int>();
            }

            return value.Split(',').Select(x => int.Parse(x.Trim())).ToList();
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
    }
}
