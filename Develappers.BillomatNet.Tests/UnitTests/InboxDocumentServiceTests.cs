// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
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
    public class InboxDocumentServiceTests : UnitTestBase<InboxDocumentService>
    {
        [Fact]
        public async Task GetById_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/inbox-documents/729809", UriKind.Relative);
            const string responseBody = "{\"inbox-document\":{\"id\":\"729809\",\"created\":\"2019-12-20T12:17:35+01:00\",\"updated\":\"2019-12-20T13:32:46+01:00\",\"user_id\":\"52821\",\"filename\":\"inbox_729809.pdf\",\"mimetype\":\"image\\/jpeg\",\"document_type\":\"RECEIPT\",\"filesize\":\"152214\",\"page_count\":\"1\",\"base64file\":\"VGVzdA==\",\"metadata\":{\"data\":[{\"key\":\"TOTAL_NET\",\"value\":\"25.5\"},{\"key\":\"CURRENCY_CODE\",\"value\":\"EUR\"},{\"key\":\"recipient\",\"value\":\"Modefriseur eG K\\u00f6nigsbr\\u00fccker Stra\\u00dfe 66 01099 Dresden\"},{\"key\":\"recipientStreet\",\"value\":\"K\\u00f6nigsbr\\u00fccker Stra\\u00dfe:66\"}]},\"customfield\":\"\"}}";
            var expectedResult = new InboxDocument
            {
                Id = 729809,
                Created = DateTime.Parse("2019-12-20T12:17:35+01:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2019-12-20T13:32:46+01:00", CultureInfo.InvariantCulture),
                UserId = 52821,
                PageCount = 1,
                FileName = "inbox_729809.pdf",
                MimeType = "image/jpeg",
                FileContent = new[] { (byte)0x54, (byte)0x65, (byte)0x73, (byte)0x74 },
                Metadata = new Dictionary<string, string>
                {
                    { "TOTAL_NET", "25.5" },
                    { "CURRENCY_CODE", "EUR" },
                    { "recipient", "Modefriseur eG Königsbrücker Straße 66 01099 Dresden" },
                    { "recipientStreet", "Königsbrücker Straße:66" }
                },
                FileSize = 152214
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.GetByIdAsync(729809);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
