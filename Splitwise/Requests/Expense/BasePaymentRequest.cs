namespace Splitwise.Requests.Expense
{
    /// <summary>
    /// Represents base payment. Use PaymentFromExistingUser and PaymentFromNewUser classes
    /// </summary>
    public abstract class BasePaymentRequest
    {
        public decimal PaidShare { get; init; }

        public decimal OwedShare { get; init; }
    }
}