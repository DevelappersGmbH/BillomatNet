// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class UnitServiceTests : UnitTestBase<UnitService>
    {
        [Fact]
        public async Task GetList_ShouldReturnCorrectValues()
        {
            // arrange
            var expectedRequestUri = new Uri("/api/units", UriKind.Relative);
            const string responseBody = "{\"units\":{\"unit\":[{\"id\":\"20573\",\"created\":\"2016-03-14T13:37:29+01:00\",\"updated\":\"2016-03-14T13:37:29+01:00\",\"name\":\"Beutel\",\"customfield\":\"\"},{\"id\":\"20574\",\"created\":\"2016-03-14T13:37:54+01:00\",\"updated\":\"2016-03-14T13:37:54+01:00\",\"name\":\"St\\u00fcck\",\"customfield\":\"\"},{\"id\":\"21813\",\"created\":\"2016-11-03T08:01:17+01:00\",\"updated\":\"2016-11-03T08:01:17+01:00\",\"name\":\"kg\",\"customfield\":\"\"}],\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"3\"}}";
            var expectedResult = new List<Unit>
            {
                new()
                {
                    Id = 20573,
                    Created = DateTime.Parse("2016-03-14T13:37:29+01:00", CultureInfo.InvariantCulture),
                    Updated = DateTime.Parse("2016-03-14T13:37:29+01:00", CultureInfo.InvariantCulture),
                    Name = "Beutel"
                },
                new()
                {
                    Id = 20574,
                    Created = DateTime.Parse("2016-03-14T13:37:54+01:00", CultureInfo.InvariantCulture),
                    Updated = DateTime.Parse("2016-03-14T13:37:54+01:00", CultureInfo.InvariantCulture),
                    Name = "Stück"
                },
                new()
                {
                    Id = 21813,
                    Created = DateTime.Parse("2016-11-03T08:01:17+01:00", CultureInfo.InvariantCulture),
                    Updated = DateTime.Parse("2016-11-03T08:01:17+01:00", CultureInfo.InvariantCulture),
                    Name = "kg"
                }
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, null, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            // act
            var result = await sut.GetListAsync();

            // assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, null, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.List.Should().SatisfyRespectively(first => first.Should().BeEquivalentTo(expectedResult[0]),
                second => second.Should().BeEquivalentTo(expectedResult[1]),
                third => third.Should().BeEquivalentTo(expectedResult[2]));
        }

        [Fact]
        public async Task CreateUnit_WithInvalidInputValue_ShouldThrowArgumentException()
        {
            // arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            // act and assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateAsync(null));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateAsync(new Unit()));
            await Assert.ThrowsAsync<ArgumentException>(() => sut.CreateAsync(new Unit { Id = 999 }));
        }
    }
}
