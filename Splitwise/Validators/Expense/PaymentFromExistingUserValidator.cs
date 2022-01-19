using FluentValidation;
using Splitwise.Requests.Expense;

namespace Splitwise.Validators.Expense
{
    public class PaymentFromExistingUserValidator : AbstractValidator<ExistingUserPayment>
    {
        public PaymentFromExistingUserValidator()
        {
            RuleFor(p => p.UserId)
                .NotNull();

            RuleFor(p => p.PaidShare)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.OwedShare)
                .GreaterThanOrEqualTo(0);
        }
    }
}