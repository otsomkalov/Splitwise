namespace Splitwise.Responses.Expense
{
    public record CreateExpenseFromSentenceResponse(
        FullExpense Expense,
        bool Valid,
        decimal Confidence,
        string Error
    );
}