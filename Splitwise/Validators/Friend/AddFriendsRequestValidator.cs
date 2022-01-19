using FluentValidation;
using Splitwise.Requests.Friend;

namespace Splitwise.Validators.Friend;

public class AddFriendsRequestValidator : AbstractValidator<AddFriendsRequest>
{
    public AddFriendsRequestValidator()
    {
        RuleForEach(r => r.Friends)
            .SetValidator(new AddFriendRequestValidator());
    }
}