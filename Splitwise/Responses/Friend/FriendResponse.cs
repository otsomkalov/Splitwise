using System;
using System.Collections.Generic;
using Splitwise.Responses.Group;
using Splitwise.Responses.Shared;

namespace Splitwise.Responses.Friend
{
    public class FriendResponse : BasePersonResponse
    {
        public IReadOnlyCollection<BalanceResponse> Balance { get; set; }

        public IReadOnlyCollection<GroupResponse> Groups { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
