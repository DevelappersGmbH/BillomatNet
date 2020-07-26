// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Develappers.BillomatNet.Api;
using ArticleTag = Develappers.BillomatNet.Types.ArticleTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Mapping
{
    internal class ArticleTagMapper : IMapper<Api.ArticleTag, ArticleTag>
    {
        public ArticleTag ApiToDomain(Api.ArticleTag value)
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

        public Api.ArticleTag DomainToApi(ArticleTag value)
        {
            if (value == null)
            {
                return null;
            }
            return new Api.ArticleTag
            {
                Id = value.Id,
                ArticleId = value.ArticleId,
                Name = value.Name
            };
        }

        public ArticleTag ApiToDomain(ArticleTagWrapper value)
        {
            return ApiToDomain(value?.ArticleTag);
        }

        public Types.PagedList<ArticleTag> ApiToDomain(ArticleTagList value)
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
                List = value.List?.Select(ApiToDomain).ToList()
            };
        }

        public Types.PagedList<ArticleTag> ApiToDomain(ArticleTagListWrapper value)
        {
            return ApiToDomain(value?.Item);
        }

        public Types.PagedList<TagCloudItem> ApiToDomain(ArticleTagCloudItemListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        public Types.PagedList<TagCloudItem> ApiToDomain(ArticleTagCloudItemList value)
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
    }
}
