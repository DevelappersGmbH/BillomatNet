using System;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Helpers
{
    internal static class MappingHelpers
    {
        /// <summary>
        /// Converts the PaymentType enum to string
        /// </summary>
        /// <param name="value">The enum</param>
        /// <returns>
        /// The string or an exception if the value doesn't match any case
        /// </returns>
        public static string ToApiValue(PaymentType value)
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
        public static string ToApiValue(InvoiceStatus value)
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
        public static PropertyType ParsePropertyType(string value)
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

        public static string PropertyTypeToString(PropertyType value)
        {
            switch (value)
            {
                case Types.PropertyType.Textarea:
                    return "TEXTAREA";
                case Types.PropertyType.Checkbox:
                    return "CHECKBOX";
                default:
                    return "TEXTFIELD";
            }
        }

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
    }
}