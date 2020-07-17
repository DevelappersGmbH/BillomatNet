using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using FakeItEasy.Configuration;
using Xunit;

namespace Develappers.BillomatNet.Tests
{
    public class ArticleServiceIntegrationTests
    {
        [Fact]
        public async Task GetArticles()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ArticleService(config);

            var result = await service.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetArticleById()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ArticleService(config);

            var result = await service.GetByIdAsync(154123);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetArticleByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ArticleService(config);

            var result = await service.GetByIdAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetArticlePropertyList()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ArticleService(config);

            var result = await service.GetPropertyListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetArticlePropertyById()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ArticleService(config);

            var result = await service.GetPropertyByIdAsync(434532, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetArticlePropertyByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ArticleService(config);

            var result = await service.GetPropertyByIdAsync(1, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetArticleTagCloud()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ArticleService(config);

            var result = await service.GetTagCloudAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetArticleTagsById()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ArticleService(config);

            var result = await service.GetTagListAsync(
                new Query<ArticleTag, ArticleTagFilter>().AddFilter(x => x.ArticleId, 434867));

            Assert.True(true);
        }

        //[Fact]
        //public async Task EditArticlePropertyCheckbox()
        //{
        //    var config = Helpers.GetTestConfiguration();
        //    var service = new ArticleService(config);

        //    var value = "1";

        //    var articlePropItem = new ArticleProperty
        //    {
        //        ArticleId = 197391,
        //        ArticlePropertyId = 2490,
        //        Value = value
        //    };

        //    var result = await service.EditArticlePropertyAsync(articlePropItem);
        //    Assert.True((bool)result.Value);
        //}

        //[Fact]
        //public async Task EditArticlePropertyText()
        //{
        //    var config = Helpers.GetTestConfiguration();
        //    var service = new ArticleService(config);

        //    var value = "xUnit test";

        //    var articlePropItem = new ArticleProperty
        //    {
        //        ArticleId = 197391,
        //        ArticlePropertyId = 2442,
        //        Value = value
        //    };

        //    var result = await service.EditArticlePropertyAsync(articlePropItem);
        //    Assert.Equal(value, result.Value);
        //}

        [Fact]
        public async Task EditArticlePropertyWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197391,
                Value = value
            };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => service.EditArticlePropertyAsync(articlePropItem));
        }
        [Fact]
        public async Task EditArticlePropertyWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ArticleService(config);

            var value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197391,
                ArticlePropertyId = 2442,
                Value = value
            };

            var result = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.EditArticlePropertyAsync(articlePropItem));
        }

        [Fact]
        public async Task EditArticlePropertyWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197395,
                ArticlePropertyId = 3250,
                Value = value
            };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => service.EditArticlePropertyAsync(articlePropItem));
        }
    }
}