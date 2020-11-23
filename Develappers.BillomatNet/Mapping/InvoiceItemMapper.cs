// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using InvoiceItem = Develappers.BillomatNet.Types.InvoiceItem;

namespace Develappers.BillomatNet.Mapping
{
    internal class InvoiceItemMapper : IMapper<Api.InvoiceItem, InvoiceItem>
    {
        public InvoiceItem ApiToDomain(Api.InvoiceItem value)
        {
            if (value == null)
            {
                return null;
            }

            IReduction reduction = null;
            if (!string.IsNullOrEmpty(value.Reduction) && value.Reduction != "null")
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

            return new InvoiceItem
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Reduction = reduction,
                InvoiceId = int.Parse(value.InvoiceId, CultureInfo.InvariantCulture),
                ArticleId = value.ArticleId.ToOptionalInt(),
                Description = value.Description,
                Position = int.Parse(value.Position, CultureInfo.InvariantCulture),
                Title = value.Title,
                Unit = value.Unit,
                TaxName = value.TaxName,
                TotalNet = float.Parse(value.TotalNet, CultureInfo.InvariantCulture),
                Quantity = float.Parse(value.Quantity, CultureInfo.InvariantCulture),
                TotalNetUnreduced = float.Parse(value.TotalNetUnreduced, CultureInfo.InvariantCulture),
                TotalGross = float.Parse(value.TotalGross, CultureInfo.InvariantCulture),
                TotalGrossUnreduced = float.Parse(value.TotalGrossUnreduced, CultureInfo.InvariantCulture),
                UnitPrice = float.Parse(value.UnitPrice, CultureInfo.InvariantCulture),
                TaxRate = value.TaxRate.ToOptionalFloat()
            };
        }

        public Api.InvoiceItem DomainToApi(InvoiceItem value)
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

            return new Api.InvoiceItem
            {
                Id = value.Id.ToString(),
                ArticleId = value.ArticleId.ToString(),
                InvoiceId = value.InvoiceId.ToString(),
                Position = value.Position.ToString(),
                Unit = value.Unit,
                Quantity = Math.Round(value.Quantity, 2).ToString(CultureInfo.InvariantCulture),
                UnitPrice = value.UnitPrice == 0 ? null : value.UnitPrice.ToString(CultureInfo.InvariantCulture),
                TaxName = value.TaxName == "" ? null : value.TaxName,
                TaxRate = value.TaxRate == 0 || value.TaxRate == null ? null : value.TaxRate.ToString(),
                Title = value.Title,
                Description = value.Description,
                Reduction = reduction,
            };
        }

        public Types.PagedList<InvoiceItem> ApiToDomain(InvoiceItemList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<InvoiceItem>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<InvoiceItem> ApiToDomain(InvoiceItemListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public InvoiceItem ApiToDomain(InvoiceItemWrapper value)
        {
            return ApiToDomain(value?.InvoiceItem);
        }
    }
}
