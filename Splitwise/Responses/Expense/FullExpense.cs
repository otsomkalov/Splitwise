using System.Collections.Generic;

namespace Splitwise.Responses.Expense
{
    public record FullExpense : Expense
    {
        public IReadOnlyCollection<Comment> Comments { get; init; }
    }
}