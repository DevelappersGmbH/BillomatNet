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
using OfferItem = Develappers.BillomatNet.Types.OfferItem;

namespace Develappers.BillomatNet.Mapping
{
    internal class OfferItemMapper : IMapper<Api.OfferItem, OfferItem>
    {
        public OfferItem ApiToDomain(Api.OfferItem value)
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

            return new OfferItem
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Reduction = reduction,
                OfferId = int.Parse(value.OfferId, CultureInfo.InvariantCulture),
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
                TaxRate = value.TaxRate.ToOptionalFloat(),
                Type = value.Type.ToOptionalItemType()
            };
        }

        public Api.OfferItem DomainToApi(OfferItem value)
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

            return new Api.OfferItem
            {
                ArticleId = value.ArticleId.ToString(),
                OfferId = value.OfferId.ToString(),
                Position = value.Position.ToString(),
                Unit = value.Unit,
                Quantity = value.Quantity.ToString(CultureInfo.InvariantCulture),
                UnitPrice = value.UnitPrice.ToString(CultureInfo.InvariantCulture),
                TaxName = value.TaxName,
                TaxRate = value.TaxRate.ToString(),
                Title = value.Title,
                Description = value.Description,
                TotalGross = value.TotalGross.ToString(CultureInfo.InvariantCulture),
                TotalNet = value.TotalNet.ToString(CultureInfo.InvariantCulture),
                Reduction = reduction,
                TotalGrossUnreduced = value.TotalGrossUnreduced.ToString(CultureInfo.InvariantCulture),
                TotalNetUnreduced = value.TotalNetUnreduced.ToString(CultureInfo.InvariantCulture),
                Type = value.Type.ToApiValue()
            };
        }

        public Types.PagedList<OfferItem> ApiToDomain(OfferItemList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<OfferItem>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<OfferItem> ApiToDomain(OfferItemListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public OfferItem ApiToDomain(OfferItemWrapper value)
        {
            return ApiToDomain(value?.OfferItem);
        }
    }
}
