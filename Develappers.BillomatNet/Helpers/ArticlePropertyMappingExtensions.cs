using System.Linq;
using Develappers.BillomatNet.Api;
using ArticleProperty = Develappers.BillomatNet.Types.ArticleProperty;

namespace Develappers.BillomatNet.Helpers
{
    internal static class ArticlePropertyMappingExtensions
    {
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

        internal static ArticleProperty ToDomain(this ArticlePropertyWrapper value)
        {
            return value?.ArticleProperty.ToDomain();
        }

        internal static Types.PagedList<ArticleProperty> ToDomain(this ArticlePropertyListWrapper value)
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
                List = value.List?.Select(ToDomain).ToList()
            };
        }

        internal static Api.ArticleProperty ToApi(this ArticleProperty value)
        {
            return new Api.ArticleProperty
            {
                Id = value.Id,
                ArticleId = value.ArticleId,
                ArticlePropertyId = value.ArticlePropertyId,
                Type = MappingHelpers.PropertyTypeToString(value.Type),
                Name = value.Name,
                Value = MappingHelpers.ParsePropertyValue(value.Type, value.Value)
            };
        }
    }
}