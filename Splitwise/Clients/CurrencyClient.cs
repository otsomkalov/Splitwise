using System.Threading.Tasks;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Responses.Currency;

namespace Splitwise.Clients
{
    public class CurrencyClient : ICurrencyClient
    {
        private readonly IRestClient _restClient;

        public CurrencyClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<CurrenciesResponse> ListAsync()
        {
            var request = new RestRequest("get_currencies");

            return _restClient.GetAsync<CurrenciesResponse>(request);
        }
    }
}