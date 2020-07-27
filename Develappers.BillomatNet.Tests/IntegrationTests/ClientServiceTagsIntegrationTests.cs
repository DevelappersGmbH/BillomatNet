// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
    public class ClientServiceTagsIntegrationTests : IntegrationTestBase<ClientService>
    {
        public ClientServiceTagsIntegrationTests() : base(c => new ClientService(c))
        {
        }

        [Fact]
        public async Task GetClientTagCloud()
        {
            await SystemUnderTest.GetTagCloudAsync(CancellationToken.None);

            Assert.True(true);
        }

        [Fact]
        public async Task GetTagListAsync()
        {
            var query = new Query<ClientTag, ClientTagFilter>()
                .AddFilter(x => x.ClientId, 796659);

            var result = await SystemUnderTest.GetTagListAsync(query);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTagListAsyncWhenArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.GetTagListAsync(null));
        }

        [Fact]
        public async Task GetTagListAsyncWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var query = new Query<ClientTag, ClientTagFilter>()
                .AddFilter(x => x.ClientId, 796659);

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetTagListAsync(query));
        }

        [Fact]
        public async Task GetTagListAsyncWhenNotFound()
        {
            var query = new Query<ClientTag, ClientTagFilter>()
                .AddFilter(x => x.ClientId, 1);

            var result = await SystemUnderTest.GetTagListAsync(query);
            Assert.Null(result.List);
        }

        [Fact]
        public async Task GetTagById()
        {
            var result = await SystemUnderTest.GetTagById(188156);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTagByIdWhenArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.GetTagById(0));
        }

        [Fact]
        public async Task GetTagByIdWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetTagById(188156));
        }

        [Fact]
        public async Task GetTagByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetTagById(100000);
            Assert.Null(result);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateTagAsync()
        {
            var tag = new ClientTag
            {
                ClientId = 796659,
                Name = "Testag"
            };

            var result = await SystemUnderTest.CreateAsync(tag);
            Assert.NotNull(result);
            //await SystemUnderTest.DeleteTagAsync(result.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateTagAsyncWhenArgumentException()
        {
            var tag = new ClientTag();

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateAsync(tag));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateTagAsyncWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var tag = new ClientTag
            {
                ClientId = 796659,
                Name = "Testag"
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.CreateAsync(tag));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
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

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteClientTagWhenArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.DeleteTagAsync(0));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteClientTagWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.DeleteTagAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteClientTagWhenNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.DeleteTagAsync(100000));
        }
    }
}
