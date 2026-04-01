// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using FreeText = Develappers.BillomatNet.Types.FreeText;

namespace Develappers.BillomatNet.Mapping
{
    internal class FreeTextMapper : IMapper<Api.FreeText, FreeText>
    {
        public FreeText ApiToDomain(Api.FreeText value)
        {
            if (value == null)
            {
                return null;
            }

            FreeTextType freeTextType;
            switch (value.FreeTextType.ToUpperInvariant())
            {
                case "INVOICE":
                    freeTextType = FreeTextType.Invoice;
                    break;
                case "CORRECTION":
                    freeTextType = FreeTextType.Correction;
                    break;
                case "OFFER":
                    freeTextType = FreeTextType.Offer;
                    break;
                case "CONFIRMATION":
                    freeTextType = FreeTextType.Confirmation;
                    break;
                case "CREDIT_NOTE":
                    freeTextType = FreeTextType.CreditNote;
                    break;
                case "DELIVERY_NOTE":
                    freeTextType = FreeTextType.DeliveryNote;
                    break;
                case "LETTER":
                    freeTextType = FreeTextType.Letter;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new FreeText
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Name = value.Name,
                FreeTextType = freeTextType,
                Title = value.Title,
                Intro = value.Intro,
                Note = value.Note,
                IsDefault = int.Parse(value.IsDefault, CultureInfo.InvariantCulture)
            };
        }

        public Api.FreeText DomainToApi(FreeText value)
        {
            if (value == null)
            {
                return null;
            }

            string freeTextType;
            switch (value.FreeTextType)
            {
                case FreeTextType.Invoice:
                    freeTextType = "INVOICE";
                    break;
                case FreeTextType.Correction:
                    freeTextType = "CORRECTION";
                    break;
                case FreeTextType.Offer:
                    freeTextType = "OFFER";
                    break;
                case FreeTextType.Confirmation:
                    freeTextType = "CONFIRMATION";
                    break;
                case FreeTextType.CreditNote:
                    freeTextType = "CREDIT_NOTE";
                    break;
                case FreeTextType.DeliveryNote:
                    freeTextType = "DELIVERY_NOTE";
                    break;
                case FreeTextType.Letter:
                    freeTextType = "LETTER";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new Api.FreeText
            {
                Id = value.Id.ToString(),
                Name = value.Name,
                FreeTextType = freeTextType,
                Title = value.Title,
                Intro = value.Intro,
                Note = value.Note,
                IsDefault = value.IsDefault.ToString(),
            };
        }

        public Types.PagedList<FreeText> ApiToDomain(FreeTextListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Types.PagedList<FreeText> ApiToDomain(FreeTextList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<FreeText>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public FreeText ApiToDomain(FreeTextWrapper value)
        {
            return ApiToDomain(value?.FreeText);
        }
    }
}
