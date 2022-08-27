using FluentValidation;
using Splitwise.Requests.Expense;

namespace Splitwise.Validators.Expense;

public class CreateSplitBySharesExpenseRequestValidator : AbstractValidator<BaseSharesSplitExpenseRequest>
{
    public CreateSplitBySharesExpenseRequestValidator()
    {
        RuleForEach(r => r.Payments)
            .SetInheritanceValidator(validator =>
            {
                validator.Add(new PaymentFromExistingUserValidator());
                validator.Add(new PaymentFromNewUserValidator());
            });
    }
}