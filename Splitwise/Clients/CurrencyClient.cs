using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Responses.Currency;

namespace Splitwise.Clients
{
    internal class CurrencyClient : ICurrencyClient
    {
        private readonly IRestClient _restClient;

        public CurrencyClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IReadOnlyCollection<Currency>> ListAsync()
        {
            var request = new RestRequest("get_currencies");
            var response = await _restClient.GetAsync<CurrenciesResponse>(request);

            return response.Currencies;
        }
    }
}