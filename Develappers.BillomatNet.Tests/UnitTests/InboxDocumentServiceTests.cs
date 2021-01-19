// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Queries;
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
                FileSize = 152214,
                DocumentType = InboxDocumentType.Receipt
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

        [Fact]
        public async Task GetList_ShouldReturnCorrectResult()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            var expectedRequestUri = new Uri("/api/inbox-documents", UriKind.Relative);
            const string expectedRequestQuery = "order_by=id+DESC&per_page=100&page=1";
            const string responseBody = "{\"suppliers\":{\"supplier\":{\"id\":\"36444\",\"created\":\"2017-06-27T17:25:46+02:00\",\"updated\":\"2018-01-20T15:23:34+01:00\",\"name\":\"Develappers GmbH\",\"salutation\":\"Herr\",\"first_name\":\"Martin\",\"last_name\":\"Hey\",\"street\":\"K\\u00f6nigsbr\\u00fccker Str. 64\",\"zip\":\"01234\",\"city\":\"Dresden\",\"state\":\"Sachsen\",\"country_code\":\"DE\",\"is_eu_country\":\"1\",\"address\":\"Develappers GmbH\\nHerr Martin Hey\\nK\\u00f6nigsbr\\u00fccker Str. 64\\n01234 Dresden\",\"phone\":\"1\",\"fax\":\"2\",\"mobile\":\"3\",\"email\":\"4@4.de\",\"www\":\"http:\\/\\/5.de\",\"tax_number\":\"123123\",\"vat_number\":\"123345\",\"bank_account_owner\":\"1\",\"bank_number\":\"456\",\"bank_name\":\"2\",\"bank_account_number\":\"123\",\"bank_swift\":\"\",\"bank_iban\":\"\",\"currency_code\":\"\",\"locale\":\"\",\"note\":\"\",\"client_number\":\"123\",\"creditor_account_number\":\"1\",\"creditor_identifier\":\"\",\"costs_gross\":\"2478.11\",\"costs_net\":\"997.99\",\"customfield\":\"\",\"supplier-property-values\":{\"supplier-property-value\":[{\"id\":\"20204\",\"supplier_id\":\"36444\",\"supplier_property_id\":\"172\",\"type\":\"CHECKBOX\",\"name\":\"SupplierCheckBox\",\"value\":\"\",\"customfield\":\"\"},{\"id\":\"20205\",\"supplier_id\":\"36444\",\"supplier_property_id\":\"173\",\"type\":\"TEXTFIELD\",\"name\":\"SupplierTextField\",\"value\":\"\",\"customfield\":\"\"},{\"id\":\"20206\",\"supplier_id\":\"36444\",\"supplier_property_id\":\"174\",\"type\":\"TEXTAREA\",\"name\":\"SupplierTextArea\",\"value\":\"\",\"customfield\":\"\"}]}},\"@page\":\"1\",\"@per_page\":\"100\",\"@total\":\"1\"}}";

            A.CallTo(() => http.GetAsync(expectedRequestUri, expectedRequestQuery, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            // ReSharper disable once RedundantArgumentDefaultValue
            // ReSharper disable once RedundantArgumentDefaultValue
            var query = new Query<InboxDocument, InboxDocumentFilter>()
                .AddSort(x => x.Id, SortOrder.Descending);
            
            var result = await sut.GetListAsync(query, CancellationToken.None);

            result.Page.Should().Be(1);
            result.ItemsPerPage.Should().Be(100);
            result.TotalItems.Should().Be(1);
            result.List.Should().SatisfyRespectively(
                first =>
                {
                    first.Id.Should().Be(36444);
                    //first.Name.Should().Be("Develappers GmbH");
                });
        }
    }
}
