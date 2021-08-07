using System.Threading.Tasks;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Requests.User;
using Splitwise.Responses.User;

namespace Splitwise.Clients
{
    public class UserClient : BaseClient, IUserClient
    {
        public UserClient(string apiKey) : base(apiKey)
        {
        }

        public Task<UserResponse> GetCurrentAsync()
        {
            var restRequest = new RestRequest("get_current_user");

            return RestClient.GetAsync<UserResponse>(restRequest);
        }

        public Task<UserResponse> GetAsync(int id)
        {
            var restRequest = new RestRequest("get_user/{id}")
                .AddUrlSegment("id", id);

            return RestClient.GetAsync<UserResponse>(restRequest);
        }

        public Task<UserResponse> UpdateAsync(int id, UpdateUserRequest request)
        {
            var restRequest = new RestRequest("update_user/{id}")
                .AddUrlSegment("id", id)
                .AddJsonBody(request);

            return RestClient.PostAsync<UserResponse>(restRequest);
        }
    }
}
