using System.Collections.Generic;

namespace Splitwise.Responses.Expense
{
    public record CreateExpenseResponse(
        IReadOnlyCollection<FullExpense> Expenses,
        IReadOnlyCollection<Error> Errors);
}