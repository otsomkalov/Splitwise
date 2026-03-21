using System.Collections.Generic;

namespace Splitwise.Responses.Expense
{
    public record UpsertExpenseResponse(
        IReadOnlyCollection<Expense> Expenses,
        Errors Errors
    );
}