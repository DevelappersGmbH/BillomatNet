// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    public class TemplateServiceIntegrationTests : IntegrationTestBase<TemplateService>
    {
        public TemplateServiceIntegrationTests() : base(c => new BillomatClient(c).Templates)
        {
        }

        [Fact]
        public async Task GetTemplatesByType()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            var query = new Query<Template, TemplateFilter>()
                .AddFilter(x => x.Type, "INVOICE");

            var result = await SystemUnderTest.GetListAsync(query, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetTemplates()
        {
            var result = await SystemUnderTest.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetTemplateById()
        {
            var result = await SystemUnderTest.GetByIdAsync(510935);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTemplateByIdWhenNotFound()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.GetByIdAsync(0));
        }
    }
}
