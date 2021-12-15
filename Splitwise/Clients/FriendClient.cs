using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Requests.Friend;
using Splitwise.Responses;
using Splitwise.Responses.Friend;

namespace Splitwise.Clients
{
    public class FriendClient : IFriendClient
    {
        private readonly IRestClient _restClient;

        public FriendClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ICollection<Friend>> ListAsync()
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

        public async Task<Friend> AddAsync(AddFriendRequest request)
        {
            var restRequest = new RestRequest("create_friend")
                .AddJsonBody(request);

            var getFriendResponse = await _restClient.PostAsync<GetFriendResponse>(restRequest);

            return getFriendResponse.Friend;
        }

        public async Task<ICollection<Friend>> AddAsync(AddFriendsRequest request)
        {
            var restRequest = new RestRequest("create_friends");

            var addFriendsResponse = await _restClient.PostAsync<AddFriendsResponse>(restRequest);

            return addFriendsResponse.Users;
        }

        public Task<DeleteFriendshipResponse> DeleteAsync(int id)
        {
            var restRequest = new RestRequest("delete_friend/{id}")
                .AddUrlSegment("id", id);

            return _restClient.PostAsync<DeleteFriendshipResponse>(restRequest);
        }
    }
}
