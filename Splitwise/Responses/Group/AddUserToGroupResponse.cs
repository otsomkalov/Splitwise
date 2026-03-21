namespace Splitwise.Responses.Group;

public record AddUserToGroupResponse(bool Success, User.User? User, Errors? Errors);