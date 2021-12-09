using System;
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
            User = new UserClient(RestClient);
        }

        protected AnonymousSplitwiseClient(string apiKey) : this()
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey), "Api key is null or empty");
            }

            RestClient = RestClient
                .AddDefaultHeader("Authorization", $"Bearer {apiKey}");
        }

        public ICurrencyClient Currency { get; }

        public ICategoryClient Category { get; }

        public IUserClient User { get; }
    }
}