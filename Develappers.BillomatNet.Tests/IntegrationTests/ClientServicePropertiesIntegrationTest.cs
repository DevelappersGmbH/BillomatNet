using Develappers.BillomatNet.Types;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [Trait(Traits.Category, Traits.Categories.IntegrationTest)]
    public class ClientServicePropertiesIntegrationTest
    {
        [Fact]
        public async Task GetClientPropertyList()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var result = await service.GetPropertyListAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientPropertyById()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var result = await service.GetPropertyById(3075686);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientPropertyByIdWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ClientService(config);

            var result = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetPropertyById(3075686));
        }

        [Fact]
        public async Task GetClientPropertyByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var result = await service.GetPropertyById(3000000);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateClientPropertyWhenCheckBox()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var clientProp = new ClientProperty
            {
                ClientId = 796659,
                ClientPropertyId = 7804,
                Value = "1"
            };

            var result = await service.EditAsync(clientProp);
            Assert.True((bool)result.Value);

            clientProp.Value = "0";
            await service.EditAsync(clientProp);
        }

        [Fact]
        public async Task EditClientPropertyWhenText()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var clientProp = new ClientProperty
            {
                ClientId = 796659,
                ClientPropertyId = 13027,
                Value = "Hello"
            };

            var result = await service.EditAsync(clientProp);
            Assert.Equal(clientProp.Value, (string)result.Value);

            clientProp.Value = "";
            await service.EditAsync(clientProp);
        }

        [Fact]
        public async Task EditClientPropertyWhenArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var clientProp = new ClientProperty { };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.EditAsync(clientProp));
        }

        [Fact]
        public async Task EditClientPropertyWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ClientService(config);

            var clientProp = new ClientProperty
            {
                ClientId = 796659,
                ClientPropertyId = 13027,
                Value = "Hello"
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.EditAsync(clientProp));
        }

        [Fact]
        public async Task EditClientPropertyWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var clientProp = new ClientProperty
            {
                ClientId = 796659,
                ClientPropertyId = 13029,
                Value = "Hello"
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.EditAsync(clientProp));
        }
    }
}
