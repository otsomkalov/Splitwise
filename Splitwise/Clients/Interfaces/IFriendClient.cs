using System.Threading.Tasks;
using Splitwise.Requests.Friend;
using Splitwise.Responses;
using Splitwise.Responses.Friend;

namespace Splitwise.Clients.Interfaces
{
    public interface IFriendClient
    {
        Task<ListFriendsResponse> ListAsync();

        Task<GetFriendResponse> GetAsync(int id);

        Task<GetFriendResponse> AddAsync(AddFriendRequest request);

        Task<AddFriendsResponse> AddAsync(AddFriendsRequest request);

        Task<DeleteFriendshipResponse> DeleteAsync(int id);
    }
}
