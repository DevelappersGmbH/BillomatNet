// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using ArticleProperty = Develappers.BillomatNet.Types.ArticleProperty;

namespace Develappers.BillomatNet.Mapping
{
    internal class ArticlePropertyMapper : IMapper<Api.ArticleProperty, ArticleProperty>
    {
        public ArticleProperty ApiToDomain(Api.ArticleProperty value)
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

        public Api.ArticleProperty DomainToApi(ArticleProperty value)
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

        public ArticleProperty ApiToDomain(ArticlePropertyWrapper value)
        {
            return ApiToDomain(value?.ArticleProperty);
        }

        public Types.PagedList<ArticleProperty> ApiToDomain(ArticlePropertyListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        private Types.PagedList<ArticleProperty> ApiToDomain(ArticlePropertyList value)
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
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }
    }
}
