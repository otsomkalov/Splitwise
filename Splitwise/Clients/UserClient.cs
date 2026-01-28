using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FluentResults;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Requests.User;
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

        public async Task<FullUser> GetCurrentAsync()
        {
            var restRequest = new RestRequest("get_current_user");

            var getUserResponse = await _restClient.GetAsync<GetUserResponse<FullUser>>(restRequest);

            return getUserResponse.User;
        }

        public async Task<User> GetAsync(int id)
        {
            var restRequest = new RestRequest("get_user/{id}")
                .AddUrlSegment("id", id);

            var getUserResponse = await _restClient.GetAsync<GetUserResponse<User>>(restRequest);

            return getUserResponse.User;
        }

        public async Task<Result<FullUser>> UpdateAsync(int id, UpdateUserRequest request)
        {
            var restRequest = new RestRequest("update_user/{id}")
                .AddUrlSegment("id", id)
                .AddJsonBody(request);

            var response = await _restClient.PostAsync<UpdateUserResponse>(restRequest);

            if (response.Errors == null)
            {
                return Result.Ok(response.User);
            }

            if (response.Errors.Base.Count != 0)
            {
                return new Result().WithErrors(response.Errors.Base);
            }

            return Result.Fail("Unknown error happened");
        }
    }
}