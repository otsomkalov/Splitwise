using FluentValidation;
using Splitwise.Requests.Group;

namespace Splitwise.Validators.Group;

public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
{
    public CreateGroupRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty();
    }
}
