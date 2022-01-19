using System.Collections.Generic;

namespace Splitwise.Responses.Friend
{
    public record AddFriendsResponse (
        IReadOnlyCollection<Friend> Users,
        Errors Errors
    );
}