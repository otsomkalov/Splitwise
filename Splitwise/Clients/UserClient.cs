using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using Newtonsoft.Json.Linq;
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

            if (response.Errors is not JObject jObject)
            {
                return Result.Ok(response.User);
            }

            if (jObject["base"] is JArray jArray)
            {
                var errors = jArray.ToObject<IEnumerable<string>>();

                var result = new Result();

                result.WithErrors(errors);

                return result;
            }

            return Result.Fail("Unknown error happened");
        }
    }
}