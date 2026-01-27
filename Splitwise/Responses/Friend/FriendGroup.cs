using System.Collections.Generic;

namespace Splitwise.Responses.Friend
{
    public record FriendGroup
    (
        int GroupId,
        IReadOnlyCollection<Balance> Balance
    );
}