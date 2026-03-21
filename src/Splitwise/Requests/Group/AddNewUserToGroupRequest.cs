namespace Splitwise.Requests.Group;

public class AddNewUserToGroupRequest : BaseAddUserToGroupRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}