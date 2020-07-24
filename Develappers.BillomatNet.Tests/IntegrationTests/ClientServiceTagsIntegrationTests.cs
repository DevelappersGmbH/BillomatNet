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
    [Trait(TraitNames.Category, CategoryNames.IntegrationTest)]
    public class ClientServiceTagsIntegrationTests : IntegrationTestBase<ClientService>
    {
        public ClientServiceTagsIntegrationTests() : base(c => new ClientService(c))
        {
        }
        [Fact]
        public async Task GetClientTagCloud()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetTagCloudAsync(CancellationToken.None);

            Assert.True(true);
        }

        [Fact]
        public async Task GetTagListAsync()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var query = new Query<ClientTag, ClientTagFilter>()
                .AddFilter(x => x.ClientId, 796659);

            var result = await service.GetTagListAsync(query);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTagListAsyncWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.GetTagListAsync(null));
        }

        [Fact]
        public async Task GetTagListAsyncWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ClientService(config);

            var query = new Query<ClientTag, ClientTagFilter>()
                .AddFilter(x => x.ClientId, 796659);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetTagListAsync(query));
        }

        [Fact]
        public async Task GetTagListAsyncWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var query = new Query<ClientTag, ClientTagFilter>()
                .AddFilter(x => x.ClientId, 1);

            var result = await service.GetTagListAsync(query);
            Assert.Null(result.List);
        }

        [Fact]
        public async Task GetTagById()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var result = await service.GetTagById(188156);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTagByIdWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.GetTagById(0));
        }

        [Fact]
        public async Task GetTagByIdWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();

            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ClientService(config);

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetTagById(188156));
        }

        [Fact]
        public async Task GetTagByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var result = await service.GetTagById(100000);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateTagAsync()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var tag = new ClientTag
            {
                ClientId = 796659,
                Name = "Testag"
            };

            var result = await service.CreateAsync(tag);
            Assert.NotNull(result);
            //await service.DeleteTagAsync(result.Id);
        }

        [Fact]
        public async Task CreateTagAsyncWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var tag = new ClientTag { };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(tag));
        }

        [Fact]
        public async Task CreateTagAsyncWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ClientService(config);

            var tag = new ClientTag
            {
                ClientId = 796659,
                Name = "Testag"
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.CreateAsync(tag));
        }

        [Fact]
        public async Task DeleteClientTag()
        {
            var tag = new ClientTag
            {
                ClientId = 796659,
                Name = "Testag"
            };

            var result = await SystemUnderTest.CreateAsync(tag);
            Assert.NotNull(await SystemUnderTest.GetTagById(result.Id));

            await SystemUnderTest.DeleteTagAsync(result.Id);
            Assert.Null(await SystemUnderTest.GetTagById(result.Id));
        }

        [Fact]
        public async Task DeleteClientTagWhenArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.DeleteTagAsync(0));
        }

        [Fact]
        public async Task DeleteClientTagWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.DeleteTagAsync(1));
        }

        [Fact]
        public async Task DeleteClientTagWhenNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.DeleteTagAsync(100000));
        }
    }
}
