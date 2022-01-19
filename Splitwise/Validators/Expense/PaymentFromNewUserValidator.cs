using FluentValidation;
using Splitwise.Requests.Expense;

namespace Splitwise.Validators.Expense
{
    public class PaymentFromNewUserValidator : AbstractValidator<NewUserPayment>
    {
        public PaymentFromNewUserValidator()
        {
            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.PaidShare)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.OwedShare)
                .GreaterThanOrEqualTo(0);
        }
    }
}