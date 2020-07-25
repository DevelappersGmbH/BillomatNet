// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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

            var type = value.Type.ToPropertyType();
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
                Type = value.Type.ToApiValue(),
                Name = value.Name,
                Value = MappingHelpers.ParsePropertyValue(value.Type, value.Value)
            };
        }
    }
}
