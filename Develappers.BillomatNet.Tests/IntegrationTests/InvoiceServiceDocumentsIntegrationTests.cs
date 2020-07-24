using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [Trait(Traits.Category, Traits.Categories.IntegrationTest)]
    public class InvoiceServiceDocumentsIntegrationTests
    {
        [Fact]
        public async Task GetInvoicePdf()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new InvoiceService(config);
            var result = await service.GetPdfAsync(1322705);

            Assert.NotNull(result);
        }
    }
}
