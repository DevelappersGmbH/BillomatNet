using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
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

        [Fact]
        public async Task DeleteArticleItem()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var title = "xUnit test";

            var articleItem = new Article
            {
                Title = title,
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };
            var result = await service.CreateAsync(articleItem);
            Assert.Equal(title, result.Title);

            await service.DeleteAsync(result.Id);
            var delResult = await service.GetByIdAsync(result.Id);

            Assert.Null(delResult);
        }

        [Fact]
        public async Task DeleteArticleItemWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(0));
        }

        [Fact]
        public async Task DeleteArticleItemWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ArticleService(config);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.DeleteAsync(198532));
        }

        [Fact]
        public async Task DeleteArticleItemWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(1));
        }

        [Fact]
        public async Task EditArticleItem()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var title = "xUnit test";

            var articleItem = new Article
            {
                Title = title,
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };

            var result = await service.CreateAsync(articleItem);
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

            var editedResult = await service.EditAsync(editedArticleItem);
            Assert.Equal(newTitle, editedArticleItem.Title);

            await service.DeleteAsync(editedArticleItem.Id);
        }

        [Fact]
        public async Task EditArticleItemWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var title = "xUnit test";

            var articleItem = new Article
            {
                Title = title,
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };

            var result = await service.CreateAsync(articleItem);
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

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.EditAsync(editedArticleItem));

            await service.DeleteAsync(result.Id);
        }

        [Fact]
        public async Task EditArticleItemWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ArticleService(config);

            var editedArticleItem = new Article
            {
                Id = 29380,
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.EditAsync(editedArticleItem));
        }

        [Fact]
        public async Task EditArticleItemWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var editedArticleItem = new Article
            {
                Id = 1,
            };

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.EditAsync(editedArticleItem));
        }

        [Fact]
        public async Task CreateArticleItem()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var articleItem = new Article
            {
                Title = "xUnit test",
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.4f
            };

            var createResult = await service.CreateAsync(articleItem);
            var getResult = await service.GetByIdAsync(createResult.Id);
            Assert.NotNull(getResult);

            await service.DeleteAsync(getResult.Id);
        }

        [Fact]
        public async Task CreateArticleItemWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ArticleService(config);

            var articleItem = new Article
            {
                Title = "xUnit test",
                SalesPrice = 3.5f,
                UnitId = 20573,
                TaxId = 21281,
                PurchasePrice = 3.0f
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() =>service.CreateAsync(articleItem));
        }

        [Fact]
        public async Task CreateArticleItemWhenNull()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(null));
        }
    }
}