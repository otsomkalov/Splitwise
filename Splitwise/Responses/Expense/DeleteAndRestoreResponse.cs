namespace Splitwise.Responses.Expense
{
    public record DeleteAndRestoreResponse(
        bool Success,
        Errors Errors
    );
}