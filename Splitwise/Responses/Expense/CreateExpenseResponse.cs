using System.Collections.Generic;

namespace Splitwise.Responses.Expense
{
    public class CreateExpenseResponse
    {
        public IEnumerable<ExpenseResponse> Expenses { get; set; }

        // public IEnumerable<Error> Errors { get; set; }
    }
}
