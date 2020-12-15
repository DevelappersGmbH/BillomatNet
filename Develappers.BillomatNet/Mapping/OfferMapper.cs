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
using Offer = Develappers.BillomatNet.Types.Offer;

namespace Develappers.BillomatNet.Mapping
{
    class OfferMapper
    {
        public Offer ApiToDomain(Api.Offer value)
        {
            if (value == null)
            {
                return null;
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

            return new Offer
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                TemplateId = value.TemplateId.ToOptionalInt(),
                CustomerPortalUrl = value.CustomerPortalUrl,
                ClientId = int.Parse(value.ClientId, CultureInfo.InvariantCulture),
                ContactId = value.ContactId.ToOptionalInt(),
                OfferNumber = value.OfferNumber,
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
                Updated = DateTime.Parse(value.Updated, CultureInfo.InvariantCulture),
                NetGross = value.NetGross.ToNetGrossType(),
                Status = value.Status.ToOfferStatus(),
                //Taxes = _taxMapper.ApiToDomain(value.Taxes),
                Quote = float.Parse(value.Quote, CultureInfo.InvariantCulture),
                Reduction = reduction,
            };
        }

        internal Types.PagedList<Offer> ApiToDomain(OfferList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Offer>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<Offer> ApiToDomain(OfferListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Offer ApiToDomain(OfferWrapper value)
        {
            return ApiToDomain(value?.Offer);
        }
    }
}
