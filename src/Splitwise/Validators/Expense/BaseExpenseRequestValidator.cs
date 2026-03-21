using FluentValidation;
using Splitwise.Requests.Expense;

namespace Splitwise.Validators.Expense
{
    internal class BaseExpenseRequestValidator : AbstractValidator<BaseExpenseRequest>
    {
        public BaseExpenseRequestValidator()
        {
            RuleFor(r => r.Cost)
                .GreaterThan(0);

            RuleFor(r => r.GroupId)
                .GreaterThanOrEqualTo(0);

            RuleFor(r => r.Description)
                .NotEmpty();

            RuleFor(r => r.CurrencyCode)
                .NotEmpty();

            RuleFor(r => r.CategoryId)
                .GreaterThanOrEqualTo(0);
        }
    }
}