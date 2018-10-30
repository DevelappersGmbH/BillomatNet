using System.Threading;
using System.Threading.Tasks;
using TaurusSoftware.BillomatNet.Queries;
using TaurusSoftware.BillomatNet.Types;
using Xunit;

namespace TaurusSoftware.BillomatNet.Tests
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
    }
}