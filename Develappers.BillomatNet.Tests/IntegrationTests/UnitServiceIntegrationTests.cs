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
    public class UnitServiceIntegrationTests : IntegrationTestBase<UnitService>
    {
        public UnitServiceIntegrationTests() : base(c => new UnitService(c))
        {
        }

        [Fact]
        public async Task GetListOfUnits()
        {
            var result = await SystemUnderTest.GetListAsync(CancellationToken.None);
            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetFilteredUnits()
        {
            var result = await SystemUnderTest.GetListAsync(
                new Query<Unit, UnitFilter>().AddFilter(x => x.Name, "Stunde"));
            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetFilteredUnitsNotFound()
        {
            var result = await SystemUnderTest.GetListAsync(
                new Query<Unit, UnitFilter>().AddFilter(x => x.Name, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
            Assert.True(result.TotalItems == 0);
        }

        [Fact]
        public async Task GetFilteredUnitsNotAuthorized()
        {
            Configuration.ApiKey = "dfgdfgd";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetListAsync(
                new Query<Unit, UnitFilter>().AddFilter(x => x.Name, "Stunde")));
        }

        [Fact]
        public async Task GetUnitById()
        {
            var result = await SystemUnderTest.GetByIdAsync(20573);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUnitByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetByIdAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUnitByIdWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new UnitService(Configuration);
            await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetByIdAsync(20573));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteUnitItem()
        {
            await SystemUnderTest.DeleteAsync(356634);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteUnitItemNotExisting()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.DeleteAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteUnitItemNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.DeleteAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditUnitItem()
        {
            var id = 1231231231;
            var newName = "xUnit test edited";

            var editedUnitItem = new Unit
            {
                Id = id,
                Name = newName,
            };

            var editedResult = await SystemUnderTest.EditAsync(editedUnitItem);
            Assert.Equal(newName, editedResult.Name);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditUnitItemWhenNotFound()
        {
            var unitItem = new Unit
            {
                Id = 1
            };
            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.EditAsync(unitItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditUnitItemWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var unitItem = new Unit
            {
                Id = 20573
            };
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.EditAsync(unitItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateUnitItem()
        {
            var name = "xUnit test";

            var unitItem = new Unit
            {
                Name = name
            };

            var result = await SystemUnderTest.CreateAsync(unitItem);
            Assert.Equal(name, result.Name);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateUnitItemWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var name = "xUnit test";

            var unitItem = new Unit
            {
                Name = name
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.CreateAsync(unitItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateUnitItemWhenNull()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateAsync(null));
        }
    }
}
