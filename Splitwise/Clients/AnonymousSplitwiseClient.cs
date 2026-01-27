using System;
using RestSharp;
using RestSharp.Serializers.Json;
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
            RestClient = new RestClient(SplitwiseUrl,
                configureSerialization: config => config.UseSystemTextJson(JsonOptions.JsonSerializerSettings));

            Currency = new CurrencyClient(RestClient);
            Category = new CategoryClient(RestClient);
            Auth = new AuthClient(RestClient);
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

        public IAuthClient Auth { get; }
    }
}