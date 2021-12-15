using System.Collections.Generic;

namespace Splitwise.Responses.Friend
{
    public record ListFriendsResponse
    (
        ICollection<Friend> Friends
    );
}