using Splitwise.Responses.Shared;

namespace Splitwise.Responses.Expense
{
    public record GetExpenseResponse : BaseResponse
    {
        public FullExpense Expense { get; init; }
    }
}