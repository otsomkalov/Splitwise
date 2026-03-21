using System.Collections.Generic;

namespace Splitwise.Responses.Group;

public record RestoreGroupResponse(bool Success, IEnumerable<string>? Errors);