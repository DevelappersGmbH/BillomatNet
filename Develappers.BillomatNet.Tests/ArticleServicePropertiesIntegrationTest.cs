using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ArticleServicePropertiesIntegrationTest : TestBase<ArticleService>
    {

        public ArticleServicePropertiesIntegrationTest() : base(c => new ArticleService(c))
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

        //[Fact]
        //public async Task EditArticlePropertyCheckbox()
        //{
        //    var value = "1";

        //    var articlePropItem = new ArticleProperty
        //    {
        //        ArticleId = 197391,
        //        ArticlePropertyId = 2490,
        //        Value = value
        //    };

        //    var result = await SystemUnderTest.EditArticlePropertyAsync(articlePropItem);
        //    Assert.True((bool)result.Value);
        //}

        //[Fact]
        //public async Task EditArticlePropertyText()
        //{
        //    var value = "xUnit test";

        //    var articlePropItem = new ArticleProperty
        //    {
        //        ArticleId = 197391,
        //        ArticlePropertyId = 2442,
        //        Value = value
        //    };

        //    var result = await SystemUnderTest.EditArticlePropertyAsync(articlePropItem);
        //    Assert.Equal(value, result.Value);
        //}

        [Fact]
        public async Task EditArticlePropertyWhenArgumentException()
        {
            var value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197391,
                Value = value
            };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditArticlePropertyAsync(articlePropItem));
        }
        [Fact]
        public async Task EditArticlePropertyWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197391,
                ArticlePropertyId = 2442,
                Value = value
            };

            var result = await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.EditArticlePropertyAsync(articlePropItem));
        }

        [Fact]
        public async Task EditArticlePropertyWhenNotFound()
        {
            var value = "xUnit test";

            var articlePropItem = new ArticleProperty
            {
                ArticleId = 197395,
                ArticlePropertyId = 3250,
                Value = value
            };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditArticlePropertyAsync(articlePropItem));
        }
    }
}
