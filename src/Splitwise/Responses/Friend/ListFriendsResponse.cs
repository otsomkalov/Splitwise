using System.Collections.Generic;

namespace Splitwise.Responses.Friend
{
    public record ListFriendsResponse
    (
        IReadOnlyCollection<Friend> Friends
    );
}