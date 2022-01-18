// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class TaxServiceIntegrationTests : IntegrationTestBase<TaxService>
    {
        public TaxServiceIntegrationTests() : base(c => new BillomatClient(c).Taxes)
        {
        }

        [Fact]
        public async Task GetListOfTaxes()
        {
            var result = await SystemUnderTest.GetListAsync(CancellationToken.None);
            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetTaxById()
        {
            var result = await SystemUnderTest.GetByIdAsync(21281);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTaxByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetByIdAsync(21285);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTaxByIdWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.GetByIdAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateTaxItem()
        {
            const string name = "xUnit Test";

            var taxItem = new Tax
            {
                Name = name,
                Rate = 1.0f,
                IsDefault = false
            };

            var result = await SystemUnderTest.CreateAsync(taxItem);
            Assert.Equal(name, result.Name);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateTaxItemWhenNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var name = "xUnit Test";

            var taxItem = new Tax
            {
                Name = name,
                Rate = 1.0f,
                IsDefault = false
            };
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.CreateAsync(taxItem));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateTaxItemWhenNull()
        {
            var tax = new Tax();
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateAsync(tax));
        }
    }
}
