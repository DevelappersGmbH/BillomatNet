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
        public async Task GetArticleTagById()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var result = await service.GetTagByIdAsync(9700);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetArticleTagByIdWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ArticleService(config);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetTagByIdAsync(9700));
        }

        [Fact]
        public async Task GetArticleTagByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var result = await service.GetTagByIdAsync(9699);
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
        public async Task DeleteArticleTag()
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

            var articleResult = await service.CreateAsync(articleItem);

            var tag = new ArticleTag
            {
                ArticleId = articleResult.Id,
                Name = "Xunit test"
            };

            var tagResult = await service.CreateTagAsync(tag);

            await service.DeleteTagAsync(tagResult.Id);
            Assert.Null(await service.GetTagByIdAsync(tagResult.Id));

            await service.DeleteAsync(articleResult.Id);
        }

        [Fact]
        public async Task DeleteArticleTagWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteTagAsync(0));
        }

        [Fact]
        public async Task DeleteArticleTagWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ArticleService(config);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.DeleteTagAsync(1));
        }

        [Fact]
        public async Task DeleteArticleTagWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteTagAsync(9699));
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

        [Fact]
        public async Task CreateArticleTag()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var name = "Xunit test";

            var tag = new ArticleTag
            {
                ArticleId = 835226,
                Name = name
            };

            var result = await service.CreateTagAsync(tag);
            Assert.Equal(name, result.Name);

            await service.DeleteTagAsync(result.Id);
        }

        [Fact]
        public async Task CreateArticleTagWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ArticleService(config);

            var tag = new ArticleTag { };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTagAsync(tag));
        }

        [Fact]
        public async Task CreateArticleTagWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();

            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ArticleService(config);

            var name = "Xunit test";

            var tag = new ArticleTag
            {
                ArticleId = 835226,
                Name = name
            };

            var result = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.CreateTagAsync(tag));
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
