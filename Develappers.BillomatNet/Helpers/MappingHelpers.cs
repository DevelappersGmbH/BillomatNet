using System;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Helpers
{
    internal static class MappingHelpers
    {
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

        public static object ParsePropertyValue(PropertyType type, string value)
        {
            if (type == PropertyType.Checkbox)
            {
                return value != "0";
            }

            return value;
        }
    }
}