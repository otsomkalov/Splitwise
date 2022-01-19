using Newtonsoft.Json;

namespace Splitwise.Requests.Expense
{
    /// <summary>
    /// Represents base payment. Use PaymentFromExistingUser and PaymentFromNewUser classes
    /// </summary>
    public abstract class BasePaymentRequest
    {
        [JsonProperty("users__{0}__paid_share")]
        public decimal PaidShare { get; init; }

        [JsonProperty("users__{0}__owed_share")]
        public decimal OwedShare { get; init; }
    }
}