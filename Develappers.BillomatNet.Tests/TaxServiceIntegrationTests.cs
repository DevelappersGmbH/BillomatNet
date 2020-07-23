using Develappers.BillomatNet.Types;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests
{
    public class TaxServiceIntegrationTests
    {
        [Fact]
        public async Task GetListOfTaxes()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new TaxService(config);
            var result = await service.GetListAsync(CancellationToken.None);
            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetByIdTax()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new TaxService(config);
            var result = await service.GetByIdAsync(21281);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTaxByIdWhenNotFound()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new TaxService(config);
            
            var result = await service.GetByIdAsync(21285);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTaxByIdWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new TaxService(config);
            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetByIdAsync(1));
        }

        //[Fact]
        //public async Task CreateTaxItem()
        //{
        //    var config = Helpers.GetTestConfiguration();
        //    var service = new TaxService(config);

        //    var name = "xUnit Test";

        //    var taxItem = new Tax
        //    {
        //        Name = name,
        //        Rate = 1.0f,
        //        IsDefault = false
        //    };

        //    var result = await service.CreateAsync(taxItem);
        //    Assert.Equal(name, result.Name);
        //}

        [Fact]
        public async Task CreateTaxItemWhenNotAuthorized()
        {
            var config = Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new TaxService(config);

            var name = "xUnit Test";

            var taxItem = new Tax
            {
                Name = name,
                Rate = 1.0f,
                IsDefault = false
            };
            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.CreateAsync(taxItem));
        }

        [Fact]
        public async Task CreateTaxItemWhenNull()
        {
            var config = Helpers.GetTestConfiguration();
            var service = new TaxService(config);

            var tax = new Tax { };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(tax));
        }
    }
}
