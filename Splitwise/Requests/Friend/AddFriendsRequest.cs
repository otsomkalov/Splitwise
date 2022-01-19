using System.Collections.Generic;

namespace Splitwise.Requests.Friend
{
    public class AddFriendsRequest
    {
        public IEnumerable<AddFriendRequest> Friends { get; init; }
    }
}