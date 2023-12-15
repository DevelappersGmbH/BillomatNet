// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Article = Develappers.BillomatNet.Types.Article;

namespace Develappers.BillomatNet.Mapping
{
    internal class ArticleMapper : IMapper<Api.Article, Article>
    {
        public Types.PagedList<Article> ApiToDomain(ArticleListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Types.PagedList<Article> ApiToDomain(ArticleList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Article>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Article ApiToDomain(Api.Article value)
        {
            if (value == null)
            {
                return null;
            }

            return new Article
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                Updated = DateTime.Parse(value.Updated, CultureInfo.InvariantCulture),
                ArticleNumber = value.ArticleNumber,
                CostCenter = value.CostCenter,
                CurrencyCode = value.CurrencyCode,
                Description = value.Description,
                Number = int.Parse(value.Number),
                NumberLength = int.Parse(value.NumberLength),
                NumberPrefix = value.NumberPre,
                PurchasePrice = value.PurchasePrice.ToOptionalFloat(),
                PurchasePriceNetGross = value.PurchasePriceNetGross.ToNetGrossType(),
                SalesPrice = value.SalesPrice.ToOptionalFloat(),
                SalesPrice2 = value.SalesPrice2.ToOptionalFloat(),
                SalesPrice3 = value.SalesPrice3.ToOptionalFloat(),
                SalesPrice4 = value.SalesPrice4.ToOptionalFloat(),
                SalesPrice5 = value.SalesPrice5.ToOptionalFloat(),
                SupplierId = value.SupplierId.ToOptionalInt(),
                TaxId = value.TaxId.ToOptionalInt(),
                Title = value.Title,
                Type = value.Type.ToOptionionalInvoiceItemType(),
                UnitId = value.UnitId.ToOptionalInt()

            };
        }

        public Api.Article DomainToApi(Article value)
        {
            if (value == null)
            {
                return null;
            }
            return new Api.Article
            {
                Id = value.Id.ToString(),
                Created = value.Created.ToApiDate(),
                ArticleNumber = value.ArticleNumber,
                CostCenter = value.CostCenter,
                CurrencyCode = value.CurrencyCode,
                Description = value.Description,
                Number = value.Number.ToString(),
                NumberLength = value.NumberLength.ToString(),
                NumberPre = value.NumberPrefix,
                PurchasePrice = value.PurchasePrice.ToInvariantString(),
                PurchasePriceNetGross = value.PurchasePriceNetGross.ToApiValue(),
                SalesPrice = value.SalesPrice.ToInvariantString(),
                SalesPrice2 = value.SalesPrice2.ToInvariantString(),
                SalesPrice3 = value.SalesPrice3.ToInvariantString(),
                SalesPrice4 = value.SalesPrice4.ToInvariantString(),
                SalesPrice5 = value.SalesPrice5.ToInvariantString(),
                SupplierId = value.SupplierId.ToString(),
                TaxId = value.TaxId.ToString(),
                Title = value.Title,
                Type = value.Type.ToApiValue(),
                UnitId = value.UnitId.ToString()
            };
        }

        public Article ApiToDomain(ArticleWrapper value)
        {
            return ApiToDomain(value?.Article);
        }
    }
}
