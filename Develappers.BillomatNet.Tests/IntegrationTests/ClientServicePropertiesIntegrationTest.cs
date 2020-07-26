// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ClientServicePropertiesIntegrationTest : IntegrationTestBase<ClientService>
    {
        public ClientServicePropertiesIntegrationTest() : base(c => new ClientService(c))
        {
        }

        [Fact]
        public async Task GetClientPropertyList()
        {
            var result = await SystemUnderTest.GetPropertyListAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientPropertyById()
        {
            var result = await SystemUnderTest.GetPropertyById(3075686);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientPropertyByIdWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetPropertyById(3075686));
        }

        [Fact]
        public async Task GetClientPropertyByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetPropertyById(3000000);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateClientPropertyWhenCheckBox()
        {
            var clientProp = new ClientProperty
            {
                ClientId = 796659,
                ClientPropertyId = 7804,
                Value = "1"
            };

            var result = await SystemUnderTest.EditAsync(clientProp);
            Assert.True((bool)result.Value);

            clientProp.Value = "0";
            await SystemUnderTest.EditAsync(clientProp);
        }

        [Fact]
        public async Task EditClientPropertyWhenText()
        {
            var clientProp = new ClientProperty
            {
                ClientId = 796659,
                ClientPropertyId = 13027,
                Value = "Hello"
            };

            var result = await SystemUnderTest.EditAsync(clientProp);
            Assert.Equal(clientProp.Value, (string)result.Value);

            clientProp.Value = "";
            await SystemUnderTest.EditAsync(clientProp);
        }

        [Fact]
        public async Task EditClientPropertyWhenArgumentException()
        {
            var clientProp = new ClientProperty();
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditAsync(clientProp));
        }

        [Fact]
        public async Task EditClientPropertyWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var clientProp = new ClientProperty
            {
                ClientId = 796659,
                ClientPropertyId = 13027,
                Value = "Hello"
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.EditAsync(clientProp));
        }

        [Fact]
        public async Task EditClientPropertyWhenNotFound()
        {
            var clientProp = new ClientProperty
            {
                ClientId = 796659,
                ClientPropertyId = 13029,
                Value = "Hello"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditAsync(clientProp));
        }
    }
}
