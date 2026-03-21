using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Errors;
using Splitwise.Requests.Friend;
using Splitwise.Responses;
using Splitwise.Responses.Friend;
using Splitwise.Validators.Friend;

namespace Splitwise.Clients
{
    internal class FriendClient : IFriendClient
    {
        private readonly IRestClient _restClient;

        public FriendClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IReadOnlyCollection<Friend>> ListAsync()
        {
            var restRequest = new RestRequest("get_friends");

            var listFriendsResponse = await _restClient.GetAsync<ListFriendsResponse>(restRequest);

            return listFriendsResponse.Friends;
        }

        public async Task<Friend> GetAsync(int id)
        {
            var restRequest = new RestRequest("get_friend/{id}")
                .AddUrlSegment("id", id);

            var getFriendResponse = await _restClient.GetAsync<GetFriendResponse>(restRequest);

            return getFriendResponse.Friend;
        }

        public async Task<Result<Friend>> AddAsync(AddFriendRequest request)
        {
            var result = new Result<Friend>();

            var validator = new AddFriendRequestValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    result.WithError(new ValidationError(error.ErrorMessage));
                }

                return result;
            }

            var restRequest = new RestRequest("create_friend")
                .AddJsonBody(request);

            var (friend, errors) = await _restClient.PostAsync<AddFriendResponse>(restRequest);

            if (errors.Base == null)
            {
                return result.WithValue(friend);
            }

            result.WithErrors(errors.Base);

            return result;
        }

        public async Task<Result<IReadOnlyCollection<Friend>>> AddAsync(AddFriendsRequest request)
        {
            var result = new Result<IReadOnlyCollection<Friend>>();

            var validator = new AddFriendsRequestValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    result.WithError(new ValidationError(error.ErrorMessage));
                }

                return result;
            }

            var restRequest = new RestRequest("create_friends");

            var (friend, errors) = await _restClient.PostAsync<AddFriendsResponse>(restRequest);

            if (errors.Base == null)
            {
                return result.WithValue(friend);
            }

            result.WithErrors(errors.Base);

            return result;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var restRequest = new RestRequest("delete_friend/{id}")
                .AddUrlSegment("id", id);

            var (_, error) = await _restClient.PostAsync<DeleteFriendshipResponse>(restRequest);

            return Result.OkIf(string.IsNullOrEmpty(error), error);
        }
    }
}