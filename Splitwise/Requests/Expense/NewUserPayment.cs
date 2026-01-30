namespace Splitwise.Requests.Expense
{
    public class NewUserPayment : BasePaymentRequest
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }
    }
}