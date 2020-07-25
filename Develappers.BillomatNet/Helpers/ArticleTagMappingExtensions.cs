// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Develappers.BillomatNet.Api;
using ArticleTag = Develappers.BillomatNet.Types.ArticleTag;
using TagCloudItem = Develappers.BillomatNet.Types.TagCloudItem;

namespace Develappers.BillomatNet.Helpers
{
    internal static class ArticleTagMappingExtensions
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

        internal static ArticleTag ToDomain(this ArticleTagWrapper value)
        {
            return value?.ArticleTag.ToDomain();
        }

        internal static Types.PagedList<TagCloudItem> ToDomain(this ArticleTagCloudItemListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<ArticleTag> ToDomain(this ArticleTagListWrapper value)
        {
            return value?.Item.ToDomain();

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


        internal static Api.ArticleTag ToApi(this ArticleTag value)
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
    }
}
