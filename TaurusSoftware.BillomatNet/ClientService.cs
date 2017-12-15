using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaurusSoftware.BillomatNet.Model;
using TaurusSoftware.BillomatNet.Net;

namespace TaurusSoftware.BillomatNet
{
    public class ClientService : ServiceBase
    {
        public ClientService(Configuration configuration): base(configuration)
        {
        }

        public async Task<Client> MyselfAsync(CancellationToken token = default(CancellationToken))
        {
            var httpClient = new HttpClient(Configuration.BillomatId, Configuration.ApiKey);
            var httpResponse = await httpClient.GetAsync(new Uri("/api/clients/myself", UriKind.Relative), token);
            var jsonModel = JsonConvert.DeserializeObject<Json.AccountWrapper>(httpResponse);
            return new Client
            {
                Id = int.Parse(jsonModel.Client.Id),
                Number = jsonModel.Client.Number,
                CountryCode = jsonModel.Client.CountryCode,
                Email = jsonModel.Client.Email,
                FirstName = jsonModel.Client.FirstName,
                LastName = jsonModel.Client.LastName,
                Note = jsonModel.Client.Note,
                Tags = jsonModel.Client.Tags.ToStringList(),
                InvoiceIds = jsonModel.Client.InvoiceId.ToIntList(),
                CreatedAt = jsonModel.Client.Created,
                IsArchived = jsonModel.Client.Archived != "0",
                NumberPrefix = jsonModel.Client.NumberPre,
                NumberLength = int.Parse(jsonModel.Client.NumberLength),
                Address = jsonModel.Client.Address,
                ClientNumber = jsonModel.Client.ClientNumber,
                BankAccountNumber = jsonModel.Client.BankAccountNumber,
                BankAccountOwner = jsonModel.Client.BankAccountOwner,
                BankIban = jsonModel.Client.BankIban,
                BankName = jsonModel.Client.BankName,
                BankNumber = jsonModel.Client.BankNumber,
                BankSwift = jsonModel.Client.BankSwift,
                City = jsonModel.Client.City,
                CurrencyCode = jsonModel.Client.CurrencyCode,
                Fax = jsonModel.Client.Fax,
                Mobile = jsonModel.Client.Mobile,
                Name = jsonModel.Client.Name,
                Phone = jsonModel.Client.Phone,
                Salutation = jsonModel.Client.Salutation,
                State = jsonModel.Client.State,
                Street = jsonModel.Client.Street,
                TaxNumber = jsonModel.Client.TaxNumber,
                VatNumber = jsonModel.Client.VatNumber,
                Web = jsonModel.Client.Www,
                ZipCode = jsonModel.Client.Zip
            };
        }
    }
}
