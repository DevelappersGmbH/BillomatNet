// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ArticleServiceTagsIntegrationTests : IntegrationTestBase<ArticleService>
    {
        public ArticleServiceTagsIntegrationTests() : base(c => new ArticleService(c))
        {
        }

        [Fact]
        public async Task GetArticleTagCloud()
        {
            var result = await SystemUnderTest.GetTagCloudAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetArticleTagById()
        {
            var result = await SystemUnderTest.GetTagByIdAsync(9700);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetArticleTagByIdWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetTagByIdAsync(9700));
        }

        [Fact]
        public async Task GetArticleTagByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetTagByIdAsync(9699);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetArticleTagsById()
        {
            await SystemUnderTest.GetTagListAsync(
                new Query<ArticleTag, ArticleTagFilter>()
                    .AddFilter(x => x.ArticleId, 434867));

            Assert.True(true);
        }

        [Fact]
        public async Task DeleteArticleTag()
        {
            var articleItem = new Article
            {
                Title = "xUnit test",
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };

            var articleResult = await SystemUnderTest.CreateAsync(articleItem);

            var tag = new ArticleTag
            {
                ArticleId = articleResult.Id,
                Name = "Xunit test"
            };

            var tagResult = await SystemUnderTest.CreateTagAsync(tag);

            await SystemUnderTest.DeleteTagAsync(tagResult.Id);
            Assert.Null(await SystemUnderTest.GetTagByIdAsync(tagResult.Id));

            await SystemUnderTest.DeleteAsync(articleResult.Id);
        }

        [Fact]
        public async Task DeleteArticleTagWhenArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.DeleteTagAsync(0));
        }

        [Fact]
        public async Task DeleteArticleTagWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.DeleteTagAsync(1));
        }

        [Fact]
        public async Task DeleteArticleTagWhenNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.DeleteTagAsync(9699));
        }

        [Fact]
        public async Task CreateArticleTag()
        {
            var name = "Xunit test";

            var tag = new ArticleTag
            {
                ArticleId = 835226,
                Name = name
            };

            var result = await SystemUnderTest.CreateTagAsync(tag);
            Assert.Equal(name, result.Name);

            await SystemUnderTest.DeleteTagAsync(result.Id);
        }

        [Fact]
        public async Task CreateArticleTagWhenArgumentException()
        {
            var tag = new ArticleTag();

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateTagAsync(tag));
        }

        [Fact]
        public async Task CreateArticleTagWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var name = "Xunit test";

            var tag = new ArticleTag
            {
                ArticleId = 835226,
                Name = name
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.CreateTagAsync(tag));
        }
    }
}
