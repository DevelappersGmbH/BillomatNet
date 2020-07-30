// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using InvoicePayment = Develappers.BillomatNet.Types.InvoicePayment;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoicePaymentMapper : IMapper<Api.InvoicePayment, InvoicePayment>
    {
        public Types.PagedList<InvoicePayment> ApiToDomain(InvoicePaymentListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        internal Types.PagedList<InvoicePayment> ApiToDomain(InvoicePaymentList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<InvoicePayment>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public InvoicePayment ApiToDomain(InvoicePaymentWrapper value)
        {
            return ApiToDomain(value?.InvoicePayment);
        }

        public InvoicePayment ApiToDomain(Api.InvoicePayment value)
        {
            if (value == null)
            {
                return null;
            }

            return new InvoicePayment
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                InvoiceId = int.Parse(value.InvoiceId, CultureInfo.InvariantCulture),
                UserId = int.Parse(value.UserId, CultureInfo.InvariantCulture),
                Date = DateTime.Parse(value.Date, CultureInfo.InvariantCulture),
                Amount = float.Parse(value.Amount, CultureInfo.InvariantCulture),
                Comment = value.Comment,
                TransactionPurpose = value.TransactionPurpose,
                CurrencyCode = value.CurrencyCode,
                Quote = float.Parse(value.Quote, CultureInfo.InvariantCulture),
                Type = value.Type.ToPaymentType(),
                MarkInvoiceAsPaid = value.MarkInvoiceAsPaid != "0"
            };
        }

        public Api.InvoicePayment DomainToApi(InvoicePayment value)
        {
            throw new NotImplementedException();
        }
    }
}
