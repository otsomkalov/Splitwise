using System;
using System.Linq;
using FluentValidation;
using Splitwise.Requests.Expense;

namespace Splitwise.Validators.Expense
{
    public class CreateExpenseRequestValidator : AbstractValidator<CreateExpenseRequest>
    {
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public CreateExpenseRequestValidator()
        {
            RuleFor(r => r.Cost)
                .GreaterThan(0);

            RuleFor(r => r.Date)
                .GreaterThan(UnixEpoch);

            RuleFor(r => r.GroupId)
                .GreaterThanOrEqualTo(0);

            RuleFor(r => r.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.CurrencyCode)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.CategoryId)
                .GreaterThanOrEqualTo(0);

            RuleFor(r => r.SplitEqually)
                .Equal(true)
                .When(r => r.Payments?.Any() != true);

            RuleFor(r => r.Payments)
                .NotNull()
                .NotEmpty()
                .When(r => r.SplitEqually != true);

            RuleForEach(r => r.Payments)
                .SetValidator(new PaymentRequestValidator());
        }
    }
}
