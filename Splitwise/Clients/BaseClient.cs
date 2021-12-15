using RestSharp;

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
