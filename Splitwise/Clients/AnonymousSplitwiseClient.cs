using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Splitwise.Clients.Interfaces;
using Splitwise.Options;

namespace Splitwise.Clients
{
    public class AnonymousSplitwiseClient : IAnonymousSplitwiseClient
    {
        private const string SplitwiseUrl = "https://secure.splitwise.com/api/v3.0/";

        protected readonly IRestClient RestClient;

        public AnonymousSplitwiseClient()
        {
            RestClient = new RestClient(SplitwiseUrl)
                .UseNewtonsoftJson(JsonOptions.JsonSerializerSettings);

            Currency = new CurrencyClient(RestClient);
            Category = new CategoryClient(RestClient);
        }

        protected AnonymousSplitwiseClient(string apiKey) : this()
        {
            RestClient = RestClient
                .AddDefaultHeader("Authorization", $"Bearer {apiKey}");
        }

        public ICurrencyClient Currency { get; }

        public ICategoryClient Category { get; }
    }
}