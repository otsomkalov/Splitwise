using Newtonsoft.Json;

namespace Splitwise.Requests.Expense
{
    public class NewUserPayment : BasePaymentRequest
    {
        [JsonProperty("users__{0}__first_name")]
        public string FirstName { get; init; }

        [JsonProperty("users__{0}__last_name")]
        public string LastName { get; init; }

        [JsonProperty("users__{0}__email")]
        public string Email { get; init; }
    }
}