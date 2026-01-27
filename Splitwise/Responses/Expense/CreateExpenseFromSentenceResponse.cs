using System.Collections.Generic;

namespace Splitwise.Responses.Expense
{
    public class CreateExpenseFromSentenceErrors : Errors
    {
        public IReadOnlyCollection<string> Cost { get; set; }

        public IReadOnlyCollection<string> Shares { get; set; }
    }

    public record CreateExpenseFromSentenceResponse(
        CreatedBySentenceExpense Expense,
        bool Valid,
        decimal Confidence,
        string Error
    );
}