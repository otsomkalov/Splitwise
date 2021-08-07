using FluentValidation;
using Splitwise.Requests.Expense;

namespace Splitwise.Validators.Expense
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty()
                .Unless(p => p.UserId.HasValue);

            RuleFor(p => p.FirstName)
                .NotNull()
                .NotEmpty()
                .Unless(p => p.UserId.HasValue);

            RuleFor(p => p.LastName)
                .NotNull()
                .NotEmpty()
                .Unless(p => p.UserId.HasValue);

            RuleFor(p => p.UserId)
                .NotNull()
                .When(p => string.IsNullOrEmpty(p.FirstName))
                .When(p => string.IsNullOrEmpty(p.LastName))
                .When(p => string.IsNullOrEmpty(p.Email));

            RuleFor(p => p.PaidShare)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.OwedShare)
                .GreaterThan(0);
        }
    }
}
