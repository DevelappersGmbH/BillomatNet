// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Mapping
{
    internal class PurchaseInvoiceMapper : IMapper<Incoming, PurchaseInvoice>
    {
        public PurchaseInvoice ApiToDomain(Incoming value)
        {
            if (value == null)
            {
                return null;
            }

            return new PurchaseInvoice
            {
                Id = value.Id.ToInt(),
                Address = value.Address.Sanitize(),
                Number = value.Number.Sanitize(),
                DueDate = value.DueDate.ToOptionalDateTime(),
                Category = value.Category.Sanitize(),
                ClientNumber = value.ClientNumber.Sanitize(),
                Created = value.Created.ToDateTime(),
                CurrencyCode = value.CurrencyCode.Sanitize(),
                Date = value.Date.ToDateTime(),
                ExpenseAccountNumber = value.ExpenseAccountNumber.Sanitize(),
                Label = value.Label.Sanitize(),
                Note = value.Note.Sanitize(),
                OpenAmount = value.OpenAmount.ToFloat(),
                PageCount = value.PageCount.ToInt(),
                PaidAmount = value.PaidAmount.ToFloat(),
                Quote = value.Quote.ToFloat(),
                Status = value.Status.ToInvoiceStatus(),
                SupplierId = value.SupplierId.ToInt(),
                TotalGross = value.TotalGross.ToFloat(),
                TotalNet = value.TotalNet.ToFloat(),
                Updated = value.Updated.ToDateTime()
            };
        }

        public Incoming DomainToApi(PurchaseInvoice value)
        {
            throw new NotImplementedException();
        }

        public PurchaseInvoice ApiToDomain(IncomingWrapper value)
        {
            return ApiToDomain(value?.Incoming);
        }
    }
}
