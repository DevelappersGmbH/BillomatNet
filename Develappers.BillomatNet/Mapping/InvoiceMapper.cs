// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using Invoice = Develappers.BillomatNet.Types.Invoice;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceMapper : IMapper<Api.Invoice, Invoice>
    {
        private readonly InvoiceTaxMapper _taxMapper = new InvoiceTaxMapper();

        public Invoice ApiToDomain(Api.Invoice value)
        {
            if (value == null)
            {
                return null;
            }

            SupplyDateType? supplyDateType;
            ISupplyDate supplyDate;
            switch (value.SupplyDateType.ToLowerInvariant())
            {
                case "supply_date":
                    supplyDateType = SupplyDateType.SupplyDate;
                    supplyDate = new DateSupplyDate
                    {
                        Date = value.SupplyDate.ToOptionalDateTime()
                    };
                    break;
                case "delivery_date":
                    supplyDateType = SupplyDateType.DeliveryDate;
                    supplyDate = new DateSupplyDate
                    {
                        Date = value.SupplyDate.ToOptionalDateTime()
                    };
                    break;
                case "supply_text":
                    supplyDateType = SupplyDateType.SupplyDate;
                    supplyDate = new FreeTextSupplyDate
                    {
                        Text = value.SupplyDate
                    };
                    break;
                case "delivery_text":
                    supplyDateType = SupplyDateType.DeliveryDate;
                    supplyDate = new FreeTextSupplyDate
                    {
                        Text = value.SupplyDate
                    };
                    break;
                case "":
                    supplyDateType = null;
                    supplyDate = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            IReduction reduction = null;
            if (!string.IsNullOrEmpty(value.Reduction))
            {
                if (value.Reduction.EndsWith("%"))
                {
                    reduction = new PercentReduction
                    {
                        Value = float.Parse(value.Reduction.Replace("%", ""), CultureInfo.InvariantCulture)
                    };
                }
                else
                {
                    reduction = new AbsoluteReduction
                    {
                        Value = float.Parse(value.Reduction, CultureInfo.InvariantCulture)
                    };
                }
            }

            return new Invoice
            {
                Address = value.Address.Sanitize(),
                ClientId = value.ClientId.ToInt(),
                ConfirmationId = value.ConfirmationId.ToOptionalInt(),
                ContactId = value.ContactId.ToOptionalInt(),
                Created = value.Created.ToDateTime(),
                CurrencyCode = value.CurrencyCode,
                CustomerPortalUrl = value.CustomerPortalUrl.Sanitize(),
                Date = value.Date.ToOptionalDateTime(),
                DiscountAmount = value.DiscountAmount.ToOptionalFloat(),
                DiscountDate = value.DiscountDate.ToOptionalDateTime(),
                DiscountDays = value.DiscountDays.ToOptionalInt(),
                DiscountRate = value.DiscountRate.ToOptionalFloat(),
                DueDate = value.DueDate.ToOptionalDateTime(),
                DueDays = value.DueDays.ToOptionalInt(),
                FreeTextId = value.FreeTextId.ToOptionalInt(),
                Id = value.Id.ToInt(),
                InvoiceId = value.InvoiceId.ToOptionalInt(),
                InvoiceNumber = value.InvoiceNumber,
                Intro = value.Intro.Sanitize(),
                Label = value.Label.Sanitize(),
                NetGross = value.NetGross.ToNetGrossType(),
                Note = value.Note.Sanitize(),
                Number = value.Number.ToOptionalInt(),
                NumberPre = value.NumberPre.Sanitize(),
                NumberLength = value.NumberLength.ToInt(),
                OfferId = value.OfferId.ToOptionalInt(),
                OpenAmount = value.OpenAmount.ToFloat(),
                PaidAmount = value.PaidAmount.ToFloat(),
                PaymentTypes = value.PaymentTypes.ToStringList(),
                Quote = value.Quote.ToFloat(),
                RecurringId = value.RecurringId.ToOptionalInt(),
                Reduction = reduction,
                SupplyDate = supplyDate,
                SupplyDateType = supplyDateType,
                Status = value.Status.ToInvoiceStatus(),
                Taxes = _taxMapper.ApiToDomain(value.Taxes),
                TemplateId = value.TemplateId.ToOptionalInt(),
                Title = value.Title.Sanitize(),
                TotalGross = value.TotalGross.ToFloat(),
                TotalGrossUnreduced = value.TotalGrossUnreduced.ToFloat(),
                TotalNet = value.TotalNet.ToFloat(),
                TotalNetUnreduced = value.TotalNetUnreduced.ToFloat(),
                Updated = value.Updated.ToDateTime()
            };
        }

        public Api.Invoice DomainToApi(Invoice value)
        {
            if (value == null)
            {
                return null;
            }

            // TODO: extract
            var reduction = "";
            switch (value.Reduction)
            {
                case null:
                    reduction = "0";
                    break;
                case PercentReduction percentReduction:
                    reduction = $"{percentReduction.Value.ToString(CultureInfo.InvariantCulture)}%";
                    break;
                case AbsoluteReduction absoluteReduction:
                    reduction = absoluteReduction.Value.ToString(CultureInfo.InvariantCulture);
                    break;
            }

            //Finds out and converts the ISupplyDate to its class and converts it to string if needed
            var strSupplyDate = "";
            switch (value.SupplyDate)
            {
                case null:
                    strSupplyDate = "";
                    break;
                case DateSupplyDate dateSupplyDate:
                    strSupplyDate = dateSupplyDate.Date.ToApiDate();
                    break;
                case FreeTextSupplyDate freeTextSupplyDate:
                    strSupplyDate = freeTextSupplyDate.Text;
                    break;
            }

            string paymentTypes = null;
            if (value.PaymentTypes != null && value.PaymentTypes.Count != 0)
            {
                paymentTypes = string.Join(",", value.PaymentTypes);
            }

            InvoiceItemsWrapper itemsWrapper = null;
            if (value.InvoiceItems != null)
            {
                itemsWrapper = new InvoiceItemsWrapper
                {
                    List = value.InvoiceItems.Select(x => x.ToApi()).ToList()
                };
            }

            return new Api.Invoice
            {
                Id = value.Id.ToApiInt(),
                Created = value.Created.ToApiDateTime(),
                Updated = value.Updated.ToApiDateTime(),
                ContactId = value.ContactId.ToApiOptionalInt(),
                ClientId = value.ClientId.ToApiInt(),
                InvoiceNumber = value.InvoiceNumber,
                Number = value.Number.ToApiOptionalInt(),
                NumberPre = value.NumberPre,
                NumberLength = value.NumberLength.ToApiOptionalInt(),
                Title = value.Title,
                Date = value.Date.ToApiDate(),
                SupplyDate = strSupplyDate,
                SupplyDateType = value.SupplyDateType.ToApiValue(),
                DueDate = value.DueDate.ToApiDate(),
                DueDays = value.DueDays.ToApiOptionalInt(),
                Address = value.Address,
                Status = value.Status.ToApiValue(),
                DiscountRate = value.DiscountRate.ToApiOptionalFloat(),
                DiscountDate = value.DiscountDate.ToApiDate(),
                DiscountDays = value.DiscountDays.ToApiOptionalInt(),
                DiscountAmount = value.DiscountAmount.ToApiOptionalFloat(),
                Label = value.Label,
                Intro = value.Intro,
                Note = value.Note,
                TotalGross = value.TotalGross.ToApiFloat(),
                TotalNet = value.TotalNet.ToApiFloat(),
                CurrencyCode = value.CurrencyCode,
                Quote = value.Quote.ToApiFloat(),
                NetGross = value.NetGross.ToApiValue(),
                Reduction = reduction,
                TotalGrossUnreduced = value.TotalGrossUnreduced.ToApiFloat(),
                TotalNetUnreduced = value.TotalNetUnreduced.ToApiFloat(),
                PaidAmount = value.PaidAmount.ToApiFloat(),
                OpenAmount = value.OpenAmount.ToApiFloat(),
                CustomerPortalUrl = value.CustomerPortalUrl,
                InvoiceId = value.InvoiceId.ToApiOptionalInt(),
                OfferId = value.OfferId.ToApiOptionalInt(),
                ConfirmationId = value.ConfirmationId.ToApiOptionalInt(),
                RecurringId = value.RecurringId.ToApiOptionalInt(),
                FreeTextId = value.FreeTextId.ToApiOptionalInt(),
                TemplateId = value.TemplateId.ToApiOptionalInt(),
                PaymentTypes = paymentTypes,
                Taxes = null,
                InvoiceItems = itemsWrapper
            };
        }

        internal Types.PagedList<Invoice> ApiToDomain(InvoiceList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Invoice>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<Invoice> ApiToDomain(InvoiceListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Invoice ApiToDomain(InvoiceWrapper value)
        {
            return ApiToDomain(value?.Invoice);
        }
    }
}
