using System.Text.Json.Serialization;

namespace Splitwise.Requests.Expense
{
    public class NewUserPayment : BasePaymentRequest
    {
        [JsonPropertyName("users__{0}__first_name")]
        public string FirstName { get; init; }

        [JsonPropertyName("users__{0}__last_name")]
        public string LastName { get; init; }

        [JsonPropertyName("users__{0}__email")]
        public string Email { get; init; }
    }
}