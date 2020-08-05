// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class InvoicePaymentQueryStringBuilder : QueryStringBuilder<InvoicePayment, Api.InvoicePayment, InvoicePaymentFilter>
    {
        protected internal override string GetFilterStringFor(InvoicePaymentFilter filter)
        {
            var filters = new List<string>();

            if (filter.InvoiceId != null)
            {
                filters.Add($"invoice_id={filter.InvoiceId.Value}");
            }
            if (filter.From != null)
            {
                filters.Add($"from={filter.From.ToApiDate()}");
            }
            if (filter.From != null)
            {
                filters.Add($"to={filter.To.ToApiDate()}");
            }
            if (filter.Type != null)
            {
                var filterValue = string.Join(",", filter.Type.Select(x => x.ToApiValue()));
                filters.Add($"type={filterValue}");
            }
            if (filter.UserId != null)
            {
                filters.Add($"user_id={filter.UserId.Value}");
            }
            return string.Join("&", filters);
        }

        private static List<string> EnumToString(List<PaymentType> values)
        {
            var list = new List<string>();
            foreach (var item in values)
            {
                switch (item)
                {
                    case PaymentType.InvoiceCorrection:
                        list.Add("Invoice_Correction");
                        break;
                    case PaymentType.CreditNote:
                        list.Add("Credit_Note");
                        break;
                    case PaymentType.BankCard:
                        list.Add("Bank_Card");
                        break;
                    case PaymentType.BankTransfer:
                        list.Add("Bank_Transfer");
                        break;
                    case PaymentType.CreditCard:
                        list.Add("Credit_Card");
                        break;
                    default:
                        list.Add(item.ToString());
                        break;
                }
            }
            return list;
        }

    }
}
