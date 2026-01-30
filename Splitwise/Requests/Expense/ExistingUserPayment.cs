
namespace Splitwise.Requests.Expense
{
    public class ExistingUserPayment : BasePaymentRequest
    {
        public int? UserId { get; init; }
    }
}