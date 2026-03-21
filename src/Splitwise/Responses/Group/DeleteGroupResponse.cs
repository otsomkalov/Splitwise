namespace Splitwise.Responses.Group;

public record DeleteGroupResponse(
    bool Success,
    Errors? Errors);