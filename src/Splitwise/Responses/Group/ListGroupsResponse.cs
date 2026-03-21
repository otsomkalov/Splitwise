using System.Collections.Generic;

namespace Splitwise.Responses.Group;

public class ListGroupsResponse
{
    public IReadOnlyCollection<Group> Groups { get; init; }
}