using System;
using System.Collections.Generic;

namespace Splitwise.Responses.Friend
{
    public record Friend : User.User
    {
        public IReadOnlyCollection<Balance> Balance { get; init; }

        public IReadOnlyCollection<FriendGroup> Groups { get; init; }

        public DateTime UpdatedAt { get; init; }
    }
}