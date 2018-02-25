using System;
using System.Globalization;
using System.Linq;
using TaurusSoftware.BillomatNet.Api;
using Article = TaurusSoftware.BillomatNet.Types.Article;
using ArticleProperty = TaurusSoftware.BillomatNet.Types.ArticleProperty;
using ArticleTag = TaurusSoftware.BillomatNet.Types.ArticleTag;
using TagCloudItem = TaurusSoftware.BillomatNet.Types.TagCloudItem;

namespace TaurusSoftware.BillomatNet.Helpers
{
    internal static class ArticleMappingExtensions
    {
        internal static PagedList<TagCloudItem> ToDomain(this ArticleTagCloudItemList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<TagCloudItem>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        private static ArticleProperty ToDomain(this Api.ArticleProperty value)
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

        internal static PagedList<Article> ToDomain(this ArticleListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static PagedList<Article> ToDomain(this ArticleList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<Article>
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

        private static Article ToDomain(this Api.Article value)
        {
            if (value == null)
            {
                return null;
            }

            return new Article
            {
                Id = int.Parse(value.Id),
                Created = DateTime.Parse(value.Created, CultureInfo.InvariantCulture),
                ArticleNumber = value.ArticleNumber,
                CurrencyCode = value.CurrencyCode,
                Description = value.Description,
                Number = int.Parse(value.Number),
                NumberLength = value.NumberLength,
                NumberPre = value.NumberPre,
                PurchasePrice = !string.IsNullOrEmpty(value.PurchasePrice) ? float.Parse(value.PurchasePrice, CultureInfo.InvariantCulture) : (float?)null,
                PurchasePriceNetGross = value.PurchasePriceNetGross,
                SalesPrice = !string.IsNullOrEmpty(value.SalesPrice) ? float.Parse(value.SalesPrice, CultureInfo.InvariantCulture) : (float?)null,
                SalesPrice2 = !string.IsNullOrEmpty(value.SalesPrice2) ? float.Parse(value.SalesPrice2, CultureInfo.InvariantCulture) : (float?)null,
                SalesPrice3 = !string.IsNullOrEmpty(value.SalesPrice3) ? float.Parse(value.SalesPrice3, CultureInfo.InvariantCulture) : (float?)null,
                SalesPrice4 = !string.IsNullOrEmpty(value.SalesPrice4) ? float.Parse(value.SalesPrice4, CultureInfo.InvariantCulture) : (float?)null,
                SalesPrice5 = !string.IsNullOrEmpty(value.SalesPrice5) ? float.Parse(value.SalesPrice5, CultureInfo.InvariantCulture) : (float?)null,
                SupplierId = !string.IsNullOrEmpty(value.SupplierId) ? int.Parse(value.SupplierId) : (int?)null,
                TaxId = !string.IsNullOrEmpty(value.TaxId) ? int.Parse(value.TaxId) : (int?)null,
                Title = value.Title,
                UnitId = !string.IsNullOrEmpty(value.UnitId) ? int.Parse(value.UnitId) : (int?)null

            };
        }

        internal static PagedList<ArticleProperty> ToDomain(this ArticlePropertyListWrapper value)
        {
            return value?.Item.ToDomain();

        }

        internal static PagedList<TagCloudItem> ToDomain(this ArticleTagCloudItemListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static PagedList<ArticleTag> ToDomain(this ArticleTagListWrapper value)
        {
            return value?.Item.ToDomain();

        }

        internal static PagedList<ArticleProperty> ToDomain(this ArticlePropertyList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<ArticleProperty>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }

        internal static PagedList<ArticleTag> ToDomain(this ArticleTagList value)
        {
            if (value == null)
            {
                return null;
            }

            return new PagedList<ArticleTag>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(x => x.ToDomain()).ToList()
            };
        }
    }
}