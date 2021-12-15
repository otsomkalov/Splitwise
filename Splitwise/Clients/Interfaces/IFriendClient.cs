using System.Collections.Generic;
using System.Threading.Tasks;
using Splitwise.Requests.Friend;
using Splitwise.Responses;
using Splitwise.Responses.Friend;

namespace Splitwise.Clients.Interfaces
{
    public interface IFriendClient
    {
        Task<ICollection<Friend>> ListAsync();

        Task<Friend> GetAsync(int id);

        Task<Friend> AddAsync(AddFriendRequest request);

        Task<ICollection<Friend>> AddAsync(AddFriendsRequest request);

        Task<DeleteFriendshipResponse> DeleteAsync(int id);
    }
}
