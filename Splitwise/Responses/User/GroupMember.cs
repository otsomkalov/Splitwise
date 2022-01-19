using System.Collections.Generic;
using Splitwise.Responses.Friend;

namespace Splitwise.Responses.User
{
    public record GroupMember : User
    {
        public IReadOnlyCollection<Balance> Balance { get; init; }
    }
}