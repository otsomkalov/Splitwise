using Splitwise.Responses.User;

namespace Splitwise.Responses.Expense
{
    public record UserPayment(
        BaseUser User,
        int UserId,
        decimal PaidShare,
        decimal OwedShare,
        decimal NetBalance
    );
}