using System.Collections.Generic;

namespace Splitwise.Responses.Friend
{
    public record GroupResponse
    (
        int GroupId,
        ICollection<BalanceResponse> Balance
    );
}