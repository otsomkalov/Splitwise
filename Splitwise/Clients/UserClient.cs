using System.Threading.Tasks;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Requests.User;
using Splitwise.Responses.Shared;
using Splitwise.Responses.User;

namespace Splitwise.Clients
{
    internal class UserClient : IUserClient
    {
        private readonly IRestClient _restClient;

        public UserClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<CurrentUserResponse> GetCurrentAsync()
        {
            var restRequest = new RestRequest("get_current_user");

            var getUserResponse = await _restClient.GetAsync<GetUserResponse<CurrentUserResponse>>(restRequest);

            return getUserResponse.User;
        }

        public async Task<BasePersonResponse> GetAsync(int id)
        {
            var restRequest = new RestRequest("get_user/{id}")
                .AddUrlSegment("id", id);

            var getUserResponse = await _restClient.GetAsync<GetUserResponse<BasePersonResponse>>(restRequest);

            return getUserResponse.User;
        }

        public Task<CurrentUserResponse> UpdateAsync(int id, UpdateUserRequest request)
        {
            var restRequest = new RestRequest("update_user/{id}")
                .AddUrlSegment("id", id)
                .AddJsonBody(request);

            return _restClient.PostAsync<CurrentUserResponse>(restRequest);
        }
    }
}
