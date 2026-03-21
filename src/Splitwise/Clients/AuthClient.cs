using System.Threading.Tasks;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Responses.Auth;

namespace Splitwise.Clients
{
    internal class AuthClient : IAuthClient
    {
        private readonly IRestClient _restClient;

        public AuthClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<TokenResponse> GetTokenAsync(string clientId, string clientSecret, string code, string redirectUrl)
        {
            var restRequest = new RestRequest("https://www.splitwise.com/oauth/token")
                .AddHeader("content-type", "application/x-www-form-urlencoded")
                .AddHeader("accept", "application/json")
                .AddParameter("application/x-www-form-urlencoded",
                    "grant_type=authorization_code&" +
                    $"client_id={clientId}&" +
                    $"client_secret={clientSecret}&" +
                    $"code={code}&" +
                    $"redirect_uri={redirectUrl}", ParameterType.RequestBody);

            var tokenResponse = await _restClient.ExecutePostAsync<TokenResponse>(restRequest);

            return tokenResponse.Data;
        }
    }
}