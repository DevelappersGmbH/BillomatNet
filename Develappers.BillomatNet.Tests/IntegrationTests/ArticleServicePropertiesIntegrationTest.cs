// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ArticleServicePropertiesIntegrationTest : IntegrationTestBase<ArticleService>
    {

        public ArticleServicePropertiesIntegrationTest() : base(c => new BillomatClient(c).Articles)
        {
        }

        [Fact]
        public async Task GetArticlePropertyList()
        {
            var result = await SystemUnderTest.GetPropertyListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetArticlePropertyById()
        {
            var result = await SystemUnderTest.GetPropertyByIdAsync(434532, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetArticlePropertyByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetPropertyByIdAsync(1, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticlePropertyCheckbox()
        {
            var value = "1";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197391,
                ArticlePropertyId = 2490,
                Value = value
            };

            var result = await SystemUnderTest.EditPropertyAsync(articlePropItem);
            Assert.True((bool)result.Value);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticlePropertyText()
        {
            var value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197391,
                ArticlePropertyId = 2442,
                Value = value
            };

            var result = await SystemUnderTest.EditPropertyAsync(articlePropItem);
            Assert.Equal(value, result.Value);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticlePropertyWhenArgumentException()
        {
            const string value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197391,
                Value = value
            };

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditPropertyAsync(articlePropItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticlePropertyWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            const string value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197391,
                ArticlePropertyId = 2442,
                Value = value
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.EditPropertyAsync(articlePropItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditArticlePropertyWhenNotFound()
        {
            const string value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197395,
                ArticlePropertyId = 3250,
                Value = value
            };

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditPropertyAsync(articlePropItem));
        }
    }
}
