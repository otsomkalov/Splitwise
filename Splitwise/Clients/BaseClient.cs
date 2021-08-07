using System;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Splitwise.Options;

namespace Splitwise.Clients
{
    public class BaseClient
    {
        protected readonly IRestClient RestClient;

        protected BaseClient(string apiKey)
        {

        }
    }
}
