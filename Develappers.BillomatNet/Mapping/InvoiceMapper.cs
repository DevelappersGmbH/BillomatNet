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


            InvoiceStatus status;
            switch (value.Status.ToLowerInvariant())
            {
                case "draft":
                    status = InvoiceStatus.Draft;
                    break;
                case "open":
                    status = InvoiceStatus.Open;
                    break;
                case "overdue":
                    status = InvoiceStatus.Overdue;
                    break;
                case "paid":
                    status = InvoiceStatus.Paid;
                    break;
                case "canceled":
                    status = InvoiceStatus.Canceled;
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
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                InvoiceId = value.InvoiceId.ToOptionalInt(),
                ConfirmationId = value.ConfirmationId.ToOptionalInt(),
                OfferId = value.OfferId.ToOptionalInt(),
                RecurringId = value.RecurringId.ToOptionalInt(),
                TemplateId = value.TemplateId.ToOptionalInt(),
                CustomerPortalUrl = value.CustomerPortalUrl,
                ClientId = int.Parse(value.ClientId, CultureInfo.InvariantCulture),
                ContactId = value.ContactId.ToOptionalInt(),
                InvoiceNumber = value.InvoiceNumber,
                Number = value.Number.ToOptionalInt(),
                NumberPre = value.NumberPre,
                NumberLength = int.Parse(value.NumberLength, CultureInfo.InvariantCulture),
                Title = value.Title,
                Date = DateTime.Parse(value.Date, CultureInfo.InvariantCulture),
                Address = value.Address,
                Label = value.Label,
                Intro = value.Intro,
                Note = value.Note,
                TotalGross = float.Parse(value.TotalGross, CultureInfo.InvariantCulture),
                TotalNet = float.Parse(value.TotalNet, CultureInfo.InvariantCulture),
                CurrencyCode = value.CurrencyCode,
                TotalGrossUnreduced = float.Parse(value.TotalGrossUnreduced, CultureInfo.InvariantCulture),
                TotalNetUnreduced = float.Parse(value.TotalNetUnreduced, CultureInfo.InvariantCulture),
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                DueDate = DateTime.Parse(value.DueDate, CultureInfo.InvariantCulture),
                DueDays = int.Parse(value.DueDays, CultureInfo.InvariantCulture),
                NetGross = value.NetGross.ToNetGrossType(),
                SupplyDate = supplyDate,
                SupplyDateType = supplyDateType,
                Status = status,
                PaymentTypes = value.PaymentTypes.ToStringList(),
                Taxes = _taxMapper.ApiToDomain(value.Taxes),
                Quote = float.Parse(value.Quote, CultureInfo.InvariantCulture),
                Reduction = reduction,
                DiscountRate = float.Parse(value.DiscountRate, CultureInfo.InvariantCulture),
                DiscountDate = value.DiscountDate.ToOptionalDateTime(),
                DiscountDays = value.DiscountDays.ToOptionalInt(),
                DiscountAmount = value.DiscountAmount.ToOptionalFloat(),
                PaidAmount = float.Parse(value.PaidAmount, CultureInfo.InvariantCulture),
                OpenAmount = float.Parse(value.OpenAmount, CultureInfo.InvariantCulture)
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

            var paymentTypes = "";
            if (value.PaymentTypes != null && value.PaymentTypes.Count != 0)
            {
                paymentTypes = String.Join(",", value.PaymentTypes);
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
                Id = value.Id.ToString(),
                Created = value.Created.ToApiDateTime(),
                Updated = value.Updated.ToApiDateTime(),
                ContactId = value.ContactId.ToString(),
                ClientId = value.ClientId.ToString(),
                InvoiceNumber = value.InvoiceNumber,
                Number = value.Number.ToString(),
                NumberPre = value.NumberPre,
                NumberLength = value.NumberLength.ToString(),
                Title = value.Title,
                Date = value.Date.ToApiDate(),
                SupplyDate = strSupplyDate,
                SupplyDateType = value.SupplyDateType.ToApiValue(),
                DueDate = value.DueDate.ToApiDate(),
                DueDays = value.DueDays.ToString(),
                Address = value.Address,
                Status = value.Status.ToApiValue(),
                DiscountRate = value.DiscountRate.ToString(CultureInfo.InvariantCulture),
                DiscountDate = value.DiscountDate.ToApiDate(),
                DiscountDays = value.DiscountDays.ToString(),
                DiscountAmount = value.DiscountAmount.ToString(),
                Label = value.Label,
                Intro = value.Intro,
                Note = value.Note,
                TotalGross = value.TotalGross.ToString(CultureInfo.InvariantCulture),
                TotalNet = value.TotalNet.ToString(CultureInfo.InvariantCulture),
                CurrencyCode = value.CurrencyCode,
                Quote = value.Quote.ToString(CultureInfo.InvariantCulture),
                NetGross = value.NetGross.ToApiValue(),
                Reduction = reduction,
                TotalGrossUnreduced = value.TotalGrossUnreduced.ToString(CultureInfo.InvariantCulture),
                TotalNetUnreduced = value.TotalNetUnreduced.ToString(CultureInfo.InvariantCulture),
                PaidAmount = value.PaidAmount.ToString(CultureInfo.InvariantCulture),
                OpenAmount = value.OpenAmount.ToString(CultureInfo.InvariantCulture),
                CustomerPortalUrl = value.CustomerPortalUrl,
                InvoiceId = value.InvoiceId.ToString(),
                OfferId = value.OfferId.ToString(),
                ConfirmationId = value.ConfirmationId.ToString(),
                RecurringId = value.RecurringId.ToString(),
                TemplateId = value.TemplateId.ToString(),
                PaymentTypes = paymentTypes,
                Taxes = new InvoiceTaxWrapper(),
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
