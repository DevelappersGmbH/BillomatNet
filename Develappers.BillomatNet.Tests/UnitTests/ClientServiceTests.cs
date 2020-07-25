// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using FakeItEasy;
using Xunit;

namespace Develappers.BillomatNet.Tests.UnitTests
{
    public class ClientServiceTests : UnitTestBase<ClientService>
    {
        [Fact]
        public async Task GetCustomerById()
        {
            const string httpResult = "{\"client\":{\"id\":\"485054\",\"created\":\"2015-02-23T09:52:05+01:00\",\"updated\":\"2018-04-17T15:16:01+02:00\",\"archived\":\"0\",\"dig_exclude\":\"0\",\"client_number\":\"KD1\",\"number\":\"1\",\"number_pre\":\"KD\",\"number_length\":\"0\",\"name\":\"Hallo GmbH\",\"salutation\":\"Herr\",\"first_name\":\"Peter\",\"last_name\":\"Acme\",\"street\":\"Acme Str. 12\",\"zip\":\"12345\",\"city\":\"M\\u00fcnchen\",\"state\":\"Sachsen\",\"country_code\":\"DE\",\"address\":\"Hallo GmbH\\nHerr Peter Acme\\nAcme Str. 12\\n12345 M\\u00fcnchen\",\"phone\":\"012312341234\",\"fax\":\"012312341235\",\"mobile\":\"012312341236\",\"email\":\"testemailvonfirma@-acme.com\",\"www\":\"www.acme.com\",\"tax_number\":\"210/211/212\",\"vat_number\":\"\",\"bank_account_owner\":\"\",\"bank_number\":\"\",\"bank_name\":\"\",\"bank_account_number\":\"\",\"bank_swift\":\"\",\"bank_iban\":\"\",\"currency_code\":\"\",\"enable_customerportal\":\"1\",\"customerportal_url\":\"https:\\/\\/develappersdev.billomat.net\\/customerportal\\/auth\\/autologin\\/entityId\\/485054?hash=bf6b673abe78a5ddbab30908d5c7bae3\",\"default_payment_types\":\"\",\"sepa_mandate\":\"\",\"sepa_mandate_date\":\"\",\"locale\":\"\",\"tax_rule\":\"COUNTRY\",\"net_gross\":\"NET\",\"price_group\":\"2\",\"debitor_account_number\":\"\",\"reduction\":\"0\",\"discount_rate_type\":\"SETTINGS\",\"discount_rate\":\"2\",\"discount_days_type\":\"SETTINGS\",\"discount_days\":\"7\",\"due_days_type\":\"SETTINGS\",\"due_days\":\"5\",\"reminder_due_days_type\":\"SETTINGS\",\"reminder_due_days\":\"7\",\"offer_validity_days_type\":\"SETTINGS\",\"offer_validity_days\":\"28\",\"dunning_run\":\"1\",\"note\":\"\",\"revenue_gross\":\"30319.68\",\"revenue_net\":\"25489.53\",\"customfield\":\"\",\"client-property-values\":{\"client-property-value\":[{\"id\":\"4139983\",\"client_id\":\"485054\",\"client_property_id\":\"7804\",\"type\":\"CHECKBOX\",\"name\":\"Stammkunde\",\"value\":\"\",\"customfield\":\"\"},{\"id\":\"4776942\",\"client_id\":\"485054\",\"client_property_id\":\"13027\",\"type\":\"TEXTFIELD\",\"name\":\"TextField\",\"value\":\"\",\"customfield\":\"\"},{\"id\":\"4777078\",\"client_id\":\"485054\",\"client_property_id\":\"13028\",\"type\":\"TEXTAREA\",\"name\":\"TextArea\",\"value\":\"\",\"customfield\":\"\"}]}}}";
            const int id = 485054;
            const string expectedUri = "/api/clients/485054";

            var http = A.Fake<IHttpClient>();
            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(httpResult));

            var sut = GetSystemUnderTest(http);
            var result = await sut.GetByIdAsync(id);

            A.CallTo(() => http.GetAsync(new Uri(expectedUri, UriKind.Relative), A<CancellationToken>.Ignored))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(id, result.Id);
            //Assert.Equal(new DateTime(2015, 2, 23, 8, 52, 5, DateTimeKind.Utc), result.Created.ToUniversalTime());
            //Assert.Equal(new DateTime(2018, 4, 17, 13, 16, 1, DateTimeKind.Utc), result.Updated.ToUniversalTime());
            //Assert.Equal(false, result.Archived);
            //Assert.Equal(false, result.DigExclude);
            Assert.Equal("KD1", result.ClientNumber);
            //Assert.Equal(1, result.Number);
            Assert.Equal("KD", result.NumberPrefix);
            Assert.Equal(0, result.NumberLength);
            Assert.Equal("Hallo GmbH", result.Name);
            Assert.Equal("Herr", result.Salutation);
            Assert.Equal("Peter", result.FirstName);
            Assert.Equal("Acme", result.LastName);
            Assert.Equal("Acme Str. 12", result.Street);
            Assert.Equal("12345", result.ZipCode);
            Assert.Equal("München", result.City);
            Assert.Equal("Sachsen", result.State);
            Assert.Equal("DE", result.CountryCode);
            Assert.Equal("Hallo GmbH\nHerr Peter Acme\nAcme Str. 12\n12345 München", result.Address);
            Assert.Equal("012312341234", result.Phone);
            Assert.Equal("012312341235", result.Fax);
            Assert.Equal("012312341236", result.Mobile);
            Assert.Equal("testemailvonfirma@-acme.com", result.Email);
            Assert.Equal("www.acme.com", result.Web);
            Assert.Equal("210/211/212", result.TaxNumber);

            /*
             *

        "vat_number": "",
        "bank_account_owner": "",
        "bank_number": "",
        "bank_name": "",
        "bank_account_number": "",
        "bank_swift": "",
        "bank_iban": "",
        "currency_code": "",
        "enable_customerportal": "1",
        "customerportal_url": "https://develappersdev.billomat.net/customerportal/auth/autologin/entityId/485054?hash=bf6b673abe78a5ddbab30908d5c7bae3",
        "default_payment_types": "",
        "sepa_mandate": "",
        "sepa_mandate_date": "",
        "locale": "",
        "tax_rule": "COUNTRY",
        "net_gross": "NET",
        "price_group": "2",
        "debitor_account_number": "",
        "reduction": "0",
        "discount_rate_type": "SETTINGS",
        "discount_rate": "2",
        "discount_days_type": "SETTINGS",
        "discount_days": "7",
        "due_days_type": "SETTINGS",
        "due_days": "5",
        "reminder_due_days_type": "SETTINGS",
        "reminder_due_days": "7",
        "offer_validity_days_type": "SETTINGS",
        "offer_validity_days": "28",
        "dunning_run": "1",
        "note": "",
        "revenue_gross": "30319.68",
        "revenue_net": "25489.53",
        "customfield": "",
        "client-property-values": {
            "client-property-value": [
                {
                    "id": "4139983",
                    "client_id": "485054",
                    "client_property_id": "7804",
                    "type": "CHECKBOX",
                    "name": "Stammkunde",
                    "value": "",
                    "customfield": ""
                },
                {
                    "id": "4776942",
                    "client_id": "485054",
                    "client_property_id": "13027",
                    "type": "TEXTFIELD",
                    "name": "TextField",
                    "value": "",
                    "customfield": ""
                },
                {
                    "id": "4777078",
                    "client_id": "485054",
                    "client_property_id": "13028",
                    "type": "TEXTAREA",
                    "name": "TextArea",
                    "value": "",
                    "customfield": ""
                }
            ]
             *
             */
        }
    }
}
