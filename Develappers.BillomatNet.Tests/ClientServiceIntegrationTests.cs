using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests
{
    public class ClientServiceIntegrationTests
    {
        [Fact]
        public async Task GetClientsByName()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);
            // ReSharper disable once RedundantArgumentDefaultValue
            var query = new Query<Client, ClientFilter>()
                .AddFilter(x => x.Name, "Regiofaktur")
                .AddSort(x => x.City, SortOrder.Ascending);

            var result = await service.GetListAsync(query, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetClients()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetListAsync(CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetClientById()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetByIdAsync(1227912);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetByIdAsync(1));
        }

        [Fact]
        public async Task GetContacts()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetContactListAsync(1227912, CancellationToken.None);

            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetContactById()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetContactByIdAsync(35641);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetContactAvatarById()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetContactAvatarByIdAsync(35641, 100);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetContactByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetContactByIdAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetClientTagCloud()
        {
            var config = Helpers.GetTestConfiguration();

            var service = new ClientService(config);

            var result = await service.GetTagCloudAsync(CancellationToken.None);

            Assert.True(true);
        }

        //[Fact]
        //public async Task CreateContact()
        //{
        //    var config = Helpers.GetTestConfiguration();
        //    var service = new ClientService(config);

        //    var contact = new Contact
        //    {
        //        ClientId = 485054,
        //        FirstName = "Test",
        //        LastName = "Testermann"
        //    };

        //    var result = await service.CreateAsync(contact);
        //    Assert.NotNull(await service.GetContactByIdAsync(result.Id));

        //    //await service.DeleteContactAsync(result.Id);
        //}

        [Fact]
        public async Task CreateContactArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var contact = new Contact{};

            var result = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(contact));
        }

        [Fact]
        public async Task CreateContactNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ClientService(config);

            var contact = new Contact
            {
                ClientId = 485054,
                FirstName = "Test",
                LastName = "Testermann"
            };

            var result = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.CreateAsync(contact));
        }

        [Fact]
        public async Task CreateContactNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var contact = new Contact
            {
                ClientId = 1,
                FirstName = "Test",
                LastName = "Testermann"
            };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(contact));
        }

        //[Fact]
        //public async Task EditContact()
        //{
        //    var config = Helpers.GetTestConfiguration();
        //    var service = new ClientService(config);

        //    var contact = new Contact
        //    {
        //        ClientId = 485054,
        //        FirstName = "Testt",
        //        LastName = "Testermann"
        //    };

        //    var result = await service.CreateAsync(contact);
        //    Assert.NotNull(await service.GetContactByIdAsync(result.Id));

        //    var editedContact = new Contact
        //    {
        //        Id = result.Id,
        //        ClientId = 485054,
        //        FirstName = "Test",
        //        LastName = result.LastName
        //    };

        //    var editedResult = await service.EditAsync(editedContact);
        //    Assert.Equal(editedContact.FirstName, editedResult.FirstName);
        //    //await service.DeleteContactAsync(result.Id);
        //}

        [Fact]
        public async Task EditContactArgumentException()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var contact = new Contact
            {
                Id = 0,
                ClientId = 485054,
                FirstName = "Testt",
                LastName = "Testermann"
            };

            var editedResult = await Assert.ThrowsAsync<ArgumentException>(() => service.EditAsync(contact));
        }

        [Fact]
        public async Task EditContactNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new ClientService(config);

            var contact = new Contact
            {
                Id = 500,
                ClientId = 485054,
                FirstName = "Testt",
                LastName = "Testermann"
            };

            var editedResult = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.EditAsync(contact));
        }

        [Fact]
        public async Task EditContactNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new ClientService(config);

            var contact = new Contact
            {
                Id = 1,
                ClientId = 485054,
                FirstName = "Testt",
                LastName = "Testermann"
            };

            var editedResult = await Assert.ThrowsAsync<NotFoundException>(() => service.EditAsync(contact));
        }
    }
}
