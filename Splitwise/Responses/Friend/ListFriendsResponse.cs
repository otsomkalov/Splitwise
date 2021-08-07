using System.Collections.Generic;
using Splitwise.Responses.User;

namespace Splitwise.Responses.Friend
{
    public class ListFriendsResponse
    {
        public IReadOnlyCollection<FriendResponse> Friends { get; set; }
    }
}