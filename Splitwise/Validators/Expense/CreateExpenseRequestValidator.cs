using System;
using System.Linq;
using FluentValidation;
using Splitwise.Requests.Expense;

namespace Splitwise.Validators.Expense
{
    internal class CreateExpenseRequestValidator : AbstractValidator<UpsertExpenseRequest>
    {
        public CreateExpenseRequestValidator()
        {
            RuleFor(r => r.Cost)
                .GreaterThan(0);

            RuleFor(r => r.Date)
                .GreaterThan(DateTime.MinValue);

            RuleFor(r => r.GroupId)
                .GreaterThanOrEqualTo(0);

            RuleFor(r => r.Description)
                .NotEmpty();

            RuleFor(r => r.CurrencyCode)
                .NotEmpty();

            RuleFor(r => r.CategoryId)
                .GreaterThanOrEqualTo(0);

            RuleFor(r => r.SplitEqually)
                .Equal(true)
                .When(r => r.Payments?.Any() != true);

            RuleFor(r => r.Payments)
                .NotEmpty()
                .When(r => r.SplitEqually != true);

            RuleForEach(r => r.Payments)
                .SetInheritanceValidator(validator =>
                {
                    validator.Add(new PaymentFromExistingUserValidator());
                    validator.Add(new PaymentFromNewUserValidator());
                });
        }
    }
}