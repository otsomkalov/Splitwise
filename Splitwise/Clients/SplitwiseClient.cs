using Splitwise.Clients.Interfaces;

namespace Splitwise.Clients
{
    public class SplitwiseClient : AnonymousSplitwiseClient, ISplitwiseClient
    {
        public SplitwiseClient(string apiKey) : base(apiKey)
        {
        }
    }
}