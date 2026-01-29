using System.Text.Json.Serialization;

namespace Splitwise.Requests.Expense
{
    /// <summary>
    /// Represents base payment. Use PaymentFromExistingUser and PaymentFromNewUser classes
    /// </summary>
    public abstract class BasePaymentRequest
    {
        [JsonPropertyName("users__{0}__paid_share")]
        public decimal PaidShare { get; init; }

        [JsonPropertyName("users__{0}__owed_share")]
        public decimal OwedShare { get; init; }
    }
}