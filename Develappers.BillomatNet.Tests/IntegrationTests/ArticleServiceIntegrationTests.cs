// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ArticleServiceIntegrationTests : IntegrationTestBase<ArticleService>
    {
        public ArticleServiceIntegrationTests() : base(c => new ArticleService(c))
        {
        }

        [Fact]
        public async Task GetArticles()
        {
            var result = await SystemUnderTest.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetArticleById()
        {
            var result = await SystemUnderTest.GetByIdAsync(154123);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetArticleByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetByIdAsync(1);
            Assert.Null(result);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteArticleItem()
        {
            var title = "xUnit test";

            var articleItem = new Article
            {
                Title = title,
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };
            var result = await SystemUnderTest.CreateAsync(articleItem);
            Assert.Equal(title, result.Title);

            await SystemUnderTest.DeleteAsync(result.Id);
            var delResult = await SystemUnderTest.GetByIdAsync(result.Id);

            Assert.Null(delResult);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteArticleItemWhenArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.DeleteAsync(0));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteArticleItemWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.DeleteAsync(198532));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteArticleItemWhenNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.DeleteAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticleItem()
        {
            var title = "xUnit test";

            var articleItem = new Article
            {
                Title = title,
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };

            var result = await SystemUnderTest.CreateAsync(articleItem);
            Assert.Equal(title, result.Title);

            var newTitle = "xUnit test edited";

            var editedArticleItem = new Article
            {
                Id = result.Id,
                Title = newTitle,
                SalesPrice = result.SalesPrice,
                UnitId = result.UnitId,
                TaxId = result.TaxId,
                PurchasePrice = result.PurchasePrice
            };

            var editedResult = await SystemUnderTest.EditAsync(editedArticleItem);
            Assert.Equal(newTitle, editedResult.Title);

            await SystemUnderTest.DeleteAsync(editedArticleItem.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticleItemWhenArgumentException()
        {
            var title = "xUnit test";

            var articleItem = new Article
            {
                Title = title,
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };

            var result = await SystemUnderTest.CreateAsync(articleItem);
            Assert.Equal(title, result.Title);

            var newTitle = "xUnit test edited";

            var editedArticleItem = new Article
            {
                Id = 0,
                Title = newTitle,
                SalesPrice = result.SalesPrice,
                UnitId = result.UnitId,
                TaxId = result.TaxId,
                PurchasePrice = result.PurchasePrice
            };

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditAsync(editedArticleItem));

            await SystemUnderTest.DeleteAsync(result.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticleItemWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var editedArticleItem = new Article
            {
                Id = 29380,
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.EditAsync(editedArticleItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticleItemWhenNotFound()
        {
            var editedArticleItem = new Article
            {
                Id = 1,
            };

            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.EditAsync(editedArticleItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateArticleItem()
        {
            var articleItem = new Article
            {
                Title = "xUnit test",
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };

            var createResult = await SystemUnderTest.CreateAsync(articleItem);
            var getResult = await SystemUnderTest.GetByIdAsync(createResult.Id);
            Assert.NotNull(getResult);

            await SystemUnderTest.DeleteAsync(getResult.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateArticleItemWhenNotAuthorized()
        {
            var articleItem = new Article
            {
                Title = "xUnit test",
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.0f
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.CreateAsync(articleItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateArticleItemWhenNull()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateAsync(null));
        }

    }
}
