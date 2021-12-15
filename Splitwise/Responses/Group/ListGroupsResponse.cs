using System.Collections.Generic;

namespace Splitwise.Responses.Group
{
    public class ListGroupsResponse
    {
        public IReadOnlyCollection<GroupResponse> Groups { get; set; }
    }
}