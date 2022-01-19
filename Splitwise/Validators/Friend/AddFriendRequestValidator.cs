using FluentValidation;
using Splitwise.Requests.Friend;

namespace Splitwise.Validators.Friend;

public class AddFriendRequestValidator : AbstractValidator<AddFriendRequest>
{
    public AddFriendRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty();
    }
}