using System;
using System.Threading.Tasks;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Requests.Friend;
using Splitwise.Responses;
using Splitwise.Responses.Friend;

namespace Splitwise.Clients
{
    public class FriendClient : BaseClient, IFriendClient
    {
        protected FriendClient(string apiKey) : base(apiKey)
        {
        }

        public Task<ListFriendsResponse> ListAsync()
        {
            var restRequest = new RestRequest("get_friends");

            return RestClient.GetAsync<ListFriendsResponse>(restRequest);
        }

        public Task<GetFriendResponse> GetAsync(int id)
        {
            var restRequest = new RestRequest("get_friend/{id}")
                .AddUrlSegment("id", id);

            return RestClient.GetAsync<GetFriendResponse>(restRequest);
        }

        public Task<GetFriendResponse> AddAsync(AddFriendRequest request)
        {
            var restRequest = new RestRequest("create_friend")
                .AddJsonBody(request);

            return RestClient.PostAsync<GetFriendResponse>(restRequest);
        }

        public Task<AddFriendsResponse> AddAsync(AddFriendsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteFriendshipResponse> DeleteAsync(int id)
        {
            var restRequest = new RestRequest("delete_friend/{id}")
                .AddUrlSegment("id", id);

            return RestClient.PostAsync<DeleteFriendshipResponse>(restRequest);
        }
    }
}
