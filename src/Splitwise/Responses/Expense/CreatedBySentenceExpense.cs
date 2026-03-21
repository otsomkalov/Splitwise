namespace Splitwise.Responses.Expense;

public record CreatedBySentenceExpense : FullExpense
{
    public CreateExpenseFromSentenceErrors Errors { get; set; }
}