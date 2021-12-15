using System.Collections.Generic;

namespace Splitwise.Responses.Friend
{
    public record AddFriendsResponse (
        ICollection<Friend> Users
    );
}