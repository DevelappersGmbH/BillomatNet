// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    public class TemplateServiceTests : UnitTestBase<TemplateService>
    {
        [Fact]
        public async Task GetById_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/templates/1", UriKind.Relative);

            const string responseBody = "{\"template\":{\"id\":\"1\",\"created\":\"2017-06-27T17:25:46+02:00\",\"updated\":\"2018-01-20T15:23:34+01:00\",\"type\":\"INVOICE\",\"template_type\":\"DEFINED\",\"name\":\"TestTemplate\",\"is_background_available\":\"0\",\"is_default\":\"0\",\"customfield\":\"\"}}";

            var expectedResult = new Template
            {
                Id = 1,
                Created = DateTime.Parse("2017-06-27T17:25:46+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2018-01-20T15:23:34+01:00", CultureInfo.InvariantCulture),
                Type = "INVOICE",
                TemplateType = "DEFINED",
                Name = "TestTemplate",
                IsBackgroundAvailable = false,
                IsDefault = false
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.GetByIdAsync(1);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            const int id = 1;
            const string expectedUri = "/api/templates/1";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetByIdAsync(id));

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetById_WithInvalidInputData_ShouldReturnArgumentException()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            //act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetByIdAsync(0));
        }
    }
}
