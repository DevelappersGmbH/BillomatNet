using System;
using System.Globalization;
using System.Linq;
using Develappers.BillomatNet.Api;
using Article = Develappers.BillomatNet.Types.Article;
using ArticleProperty = Develappers.BillomatNet.Types.ArticleProperty;
using ArticleTag = Develappers.BillomatNet.Types.ArticleTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Helpers
{
    internal static class ArticleMappingExtensions
    {
        internal static Types.PagedList<TagCloudItem> ToDomain(this ArticleTagCloudItemList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<TagCloudItem>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        private static ArticleProperty ToDomain(this Develappers.BillomatNet.Api.ArticleProperty value)
        {
            if (value == null)
            {
                return null;
            }

            var type = MappingHelpers.ParsePropertyType(value.Type);
            return new ArticleProperty
            {
                Id = value.Id,
                ArticlePropertyId = value.ArticlePropertyId,
                Type = type,
                ArticleId = value.ArticleId,
                Name = value.Name,
                Value = MappingHelpers.ParsePropertyValue(type, value.Value)
            };
        }

        private static ArticleTag ToDomain(this Api.ArticleTag value)
        {
            if (value == null)
            {
                return null;
            }

            return new ArticleTag
            {
                Id = value.Id,
                ArticleId = value.ArticleId,
                Name = value.Name
            };
        }

        internal static Types.PagedList<Article> ToDomain(this ArticleListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<Article> ToDomain(this ArticleList value)
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
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static Article ToDomain(this ArticleWrapper value)
        {
            return value?.Article.ToDomain();
        }

        internal static ArticleProperty ToDomain(this ArticlePropertyWrapper value)
        {
            return value?.ArticleProperty.ToDomain();
        }

        internal static ArticleTag ToDomain(this ArticleTagWrapper value)
        {
            return value?.ArticleTag.ToDomain();
        }

        private static Article ToDomain(this Develappers.BillomatNet.Api.Article value)
        {
            if (value == null)
            {
                return null;
            }

            return new Article
            {
                Id = int.Parse(value.Id, CultureInfo.InvariantCulture),
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                ArticleNumber = value.ArticleNumber,
                CurrencyCode = value.CurrencyCode,
                Description = value.Description,
                Number = int.Parse(value.Number),
                NumberLength = value.NumberLength,
                NumberPre = value.NumberPre,
                PurchasePrice = value.PurchasePrice.ToOptionalFloat(),
                PurchasePriceNetGross = value.PurchasePriceNetGross,
                SalesPrice = value.SalesPrice.ToOptionalFloat(),
                SalesPrice2 = value.SalesPrice2.ToOptionalFloat(),
                SalesPrice3 = value.SalesPrice3.ToOptionalFloat(),
                SalesPrice4 = value.SalesPrice4.ToOptionalFloat(),
                SalesPrice5 = value.SalesPrice5.ToOptionalFloat(),
                SupplierId = value.SupplierId.ToOptionalInt(),
                TaxId = value.TaxId.ToOptionalInt(),
                Title = value.Title,
                UnitId = value.UnitId.ToOptionalInt()

            };
        }

        internal static Types.PagedList<ArticleProperty> ToDomain(this ArticlePropertyListWrapper value)
        {
            return value?.Item.ToDomain();

        }

        internal static Types.PagedList<TagCloudItem> ToDomain(this ArticleTagCloudItemListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<ArticleTag> ToDomain(this ArticleTagListWrapper value)
        {
            return value?.Item.ToDomain();

        }

        internal static Types.PagedList<ArticleProperty> ToDomain(this ArticlePropertyList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<ArticleProperty>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static Types.PagedList<ArticleTag> ToDomain(this ArticleTagList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<ArticleTag>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static Api.Article ToApi(this Article value)
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
                CurrencyCode = value.CurrencyCode,
                Description = value.Description,
                Number = value.Number.ToString(),
                NumberLength = value.NumberLength,
                NumberPre = value.NumberPre,
                PurchasePrice = value.PurchasePrice.ToInvariantString(),
                PurchasePriceNetGross = value.PurchasePriceNetGross,
                SalesPrice = value.SalesPrice.ToInvariantString(),
                SalesPrice2 = value.SalesPrice2.ToInvariantString(),
                SalesPrice3 = value.SalesPrice3.ToInvariantString(),
                SalesPrice4 = value.SalesPrice4.ToInvariantString(),
                SalesPrice5 = value.SalesPrice5.ToInvariantString(),
                SupplierId = value.SupplierId.ToString(),
                TaxId = value.TaxId.ToString(),
                Title = value.Title,
                UnitId = value.UnitId.ToString()
            };
        }
    }
}