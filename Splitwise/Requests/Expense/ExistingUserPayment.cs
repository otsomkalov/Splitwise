
using System.Text.Json.Serialization;

namespace Splitwise.Requests.Expense
{
    public class ExistingUserPayment : BasePaymentRequest
    {
        [JsonPropertyName("users__{0}__user_id")]
        public int? UserId { get; init; }
    }
}