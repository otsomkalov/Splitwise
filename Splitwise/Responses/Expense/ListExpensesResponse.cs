using System.Collections.Generic;

namespace Splitwise.Responses.Expense
{
    internal class ListExpensesResponse
    {
        public IReadOnlyCollection<FullExpense> Expenses { get; init; }
    }
}