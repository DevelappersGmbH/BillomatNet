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
    public class ClientServiceContactsIntegrationTests : IntegrationTestBase<ClientService>
    {
        public ClientServiceContactsIntegrationTests() : base(c => new BillomatClient(c).Clients)
        {
        }

        [Fact]
        public async Task GetContacts()
        {
            var result = await SystemUnderTest.GetContactListAsync(1227912, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetContactById()
        {
            var result = await SystemUnderTest.GetContactByIdAsync(35641);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetContactAvatarById()
        {
            var result = await SystemUnderTest.GetContactAvatarByIdAsync(35641, 100);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetContactByIdWhenNotFound()
        {
            var result = await SystemUnderTest.GetContactByIdAsync(1);
            Assert.Null(result);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteContact()
        {
            var contact = new Contact
            {
                ClientId = 485054,
                FirstName = "Test",
                LastName = "Testermann"
            };

            var result = await SystemUnderTest.CreateAsync(contact);
            Assert.NotNull(await SystemUnderTest.GetContactByIdAsync(result.Id));

            await SystemUnderTest.DeleteContactAsync(result.Id);

            var result2 = await SystemUnderTest.GetContactByIdAsync(result.Id);
            Assert.Null(result2);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteContactArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.DeleteContactAsync(0));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteContactNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.DeleteContactAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task DeleteContactNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.DeleteContactAsync(1));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateContact()
        {
            var contact = new Contact
            {
                ClientId = 485054,
                FirstName = "Test",
                LastName = "Testermann"
            };

            var result = await SystemUnderTest.CreateAsync(contact);
            Assert.NotNull(await SystemUnderTest.GetContactByIdAsync(result.Id));

            await SystemUnderTest.DeleteContactAsync(result.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateContactArgumentException()
        {
            var contact = new Contact();

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateAsync(contact));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateContactNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";

            var contact = new Contact
            {
                ClientId = 485054,
                FirstName = "Test",
                LastName = "Testermann"
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.CreateAsync(contact));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task CreateContactNotFound()
        {
            var contact = new Contact
            {
                ClientId = 1,
                FirstName = "Test",
                LastName = "Testermann"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.CreateAsync(contact));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditContact()
        {
            var contact = new Contact
            {
                ClientId = 485054,
                FirstName = "Testt",
                LastName = "Testermann"
            };

            var result = await SystemUnderTest.CreateAsync(contact);
            Assert.NotNull(await SystemUnderTest.GetContactByIdAsync(result.Id));

            var editedContact = new Contact
            {
                Id = result.Id,
                ClientId = 485054,
                FirstName = "Test",
                LastName = result.LastName
            };

            var editedResult = await SystemUnderTest.EditAsync(editedContact);
            Assert.Equal(editedContact.FirstName, editedResult.FirstName);
            await SystemUnderTest.DeleteContactAsync(result.Id);
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditContactArgumentException()
        {
            var contact = new Contact
            {
                Id = 0,
                ClientId = 485054,
                FirstName = "Testt",
                LastName = "Testermann"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => SystemUnderTest.EditAsync(contact));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditContactNotAuthorized()
        {
            Configuration.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var contact = new Contact
            {
                Id = 500,
                ClientId = 485054,
                FirstName = "Testt",
                LastName = "Testermann"
            };

            await Assert.ThrowsAsync<NotAuthorizedException>(() => SystemUnderTest.EditAsync(contact));
        }

        [Fact(Skip = "Write operations shouldn't run unattended. Use unit test instead.")]
        public async Task EditContactNotFound()
        {
            var contact = new Contact
            {
                Id = 1,
                ClientId = 485054,
                FirstName = "Testt",
                LastName = "Testermann"
            };

            await Assert.ThrowsAsync<NotFoundException>(() => SystemUnderTest.EditAsync(contact));
        }
    }
}
