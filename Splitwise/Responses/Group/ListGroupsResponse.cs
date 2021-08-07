using System.Collections.Generic;
using Splitwise.Clients.Interfaces;

namespace Splitwise.Responses.Group
{
    public class ListGroupsResponse
    {
        public IReadOnlyCollection<GroupResponse> Groups { get; set; }
    }
}