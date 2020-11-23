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
using Develappers.BillomatNet.Tests.UnitTests.Comparer;
using Develappers.BillomatNet.Types;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class SupplierServiceTests : UnitTestBase<SupplierService>
    {
        [Fact]
        public async Task GetById_ShouldReturnCorrectValues()
        {
            //arrange
            var expectedRequestUri = new Uri("/api/suppliers/4662801", UriKind.Relative);
            const string responseBody = "{\"supplier\":{\"id\":\"36444\",\"created\":\"2017-06-27T17:25:46+02:00\",\"updated\":\"2018-01-20T15:23:34+01:00\",\"name\":\"Develappers GmbH\",\"salutation\":\"Herr\",\"first_name\":\"Martin\",\"last_name\":\"Hey\",\"street\":\"Königsbrücker Str. 64\",\"zip\":\"01234\",\"city\":\"Dresden\",\"state\":\"Sachsen\",\"country_code\":\"DE\",\"is_eu_country\":\"1\",\"address\":\"Develappers GmbH\nHerr Martin Hey\nKönigsbrücker Str. 64\n01234 Dresden\",\"phone\":\"1\",\"fax\":\"2\",\"mobile\":\"3\",\"email\":\"4@4.de\",\"www\":\"http:\\/\\/5.de\",\"tax_number\":\"123123\",\"vat_number\":\"123345\",\"bank_account_owner\":\"1\",\"bank_number\":\"456\",\"bank_name\":\"2\",\"bank_account_number\":\"123\",\"bank_swift\":\"\",\"bank_iban\":\"\",\"currency_code\":\"\",\"locale\":\"\",\"note\":\"\",\"client_number\":\"123\",\"creditor_account_number\":\"1\",\"creditor_identifier\":\"\",\"costs_gross\":\"2478.11\",\"costs_net\":\"997.99\",\"customfield\":\"\",\"supplier-property-values\":{\"supplier-property-value\":[{\"id\":\"20204\",\"supplier_id\":\"36444\",\"supplier_property_id\":\"172\",\"type\":\"CHECKBOX\",\"name\":\"SupplierCheckBox\",\"value\":\"\",\"customfield\":\"\"},{\"id\":\"20205\",\"supplier_id\":\"36444\",\"supplier_property_id\":\"173\",\"type\":\"TEXTFIELD\",\"name\":\"SupplierTextField\",\"value\":\"\",\"customfield\":\"\"},{\"id\":\"20206\",\"supplier_id\":\"36444\",\"supplier_property_id\":\"174\",\"type\":\"TEXTAREA\",\"name\":\"SupplierTextArea\",\"value\":\"\",\"customfield\":\"\"}]}}}";
            var expectedResult = new Supplier
            {
                Id = 36444,
                Created = DateTime.Parse("2017-06-27T17:25:46+02:00", CultureInfo.InvariantCulture),
                Updated = DateTime.Parse("2018-01-20T15:23:34+01:00", CultureInfo.InvariantCulture),
                Name = "Develappers GmbH",
                Salutation = "Herr",
                FirstName = "Martin",
                LastName = "Hey",
                Street = "Königsbrücker Str. 64",
                Zip = "01234",
                City = "Dresden",
                State = "Sachsen",
                CountryCode = "DE",
                IsEuCountry = true,
                Address = "Develappers GmbH\nHerr Martin Hey\nKönigsbrücker Str. 64\n01234 Dresden",
                Phone = "1",
                Fax = "2",
                Mobile = "3",
                Email = "4@4.de",
                Www = "http://5.de",
                TaxNumber = "123123",
                VatNumber = "123345",
                BankAccountOwner = "1",
                BankNumber = "456",
                BankName = "2",
                BankAccountNumber = "123",
                BankSwift = "",
                BankIban = "",
                CurrencyCode = "",
                Locale = "",
                Note = "",
                ClientNumber = "123",
                CreditorAccountNumber = "1",
                CreditorIdentifier = "",
                CostsGross = 2478.11f,
                CostsNet = 997.99f,
                SupplierPropertyValues = new List<SupplierPropertyValue>
                {
                    new SupplierPropertyValue
                    {
                        Id = 20204,
                        SupplierId = 36444,
                        SupplierPropertyId = 172,
                        Type = PropertyType.Checkbox,
                        Name = "SupplierCheckBox",
                        Value = true,
                    },
                    new SupplierPropertyValue
                    {
                        Id = 20205,
                        SupplierId = 36444,
                        SupplierPropertyId = 173,
                        Type = PropertyType.Textfield,
                        Name = "SupplierTextField",
                        Value = "",
                    },
                    new SupplierPropertyValue
                    {
                        Id = 20206,
                        SupplierId = 36444,
                        SupplierPropertyId = 174,
                        Type = PropertyType.Textarea,
                        Name = "SupplierTextArea",
                        Value = "",
                    }
                }
            };

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(responseBody));

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.GetByIdAsync(4662801);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentUsingComparerTo(expectedResult, new SupplierEqualityComparer());
        }

        [Fact]
        public async Task GetById_WithInvalidCredentials_ShouldThrowNotAuthorizedException()
        {
            const int id = 485054;
            const string expectedUri = "/api/suppliers/485054";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotAuthorizedException);

            var sut = GetSystemUnderTest(http);
            await Assert.ThrowsAsync<NotAuthorizedException>(() => sut.GetByIdAsync(id));

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetById__WithInvalidInputData_ShouldReturnArgumentException()
        {
            //arrange
            var http = A.Fake<IHttpClient>();
            var sut = GetSystemUnderTest(http);

            //act and assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetByIdAsync(0));
        }

        [Fact]
        public async Task GetCommentById_WithInvalidId_ShouldThrowNotFoundException()
        {
            //arrange
            const int id = 1;
            var expectedRequestUri = new Uri("/api/suppliers/1", UriKind.Relative);

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .ThrowsAsync(ExceptionFactory.CreateNotFoundException());

            var sut = GetSystemUnderTest(http);

            //act
            var result = await sut.GetByIdAsync(id);

            //assert
            A.CallTo(() => http.GetAsync(expectedRequestUri, A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            Assert.Null(result);
        }
    }
}
