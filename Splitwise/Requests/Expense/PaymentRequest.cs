using Newtonsoft.Json;

namespace Splitwise.Requests.Expense
{
    public class PaymentRequest
    {
        [JsonProperty("users__{0}__user_id")]
        public int? UserId { get; set; }

        [JsonProperty("users__{0}__paid_share")]
        public double PaidShare { get; set; }

        [JsonProperty("users__{0}__owed_share")]
        public double OwedShare { get; set; }

        [JsonProperty("users__{0}__first_name")]
        public string FirstName { get; set; }

        [JsonProperty("users__{0}__last_name")]
        public string LastName { get; set; }

        [JsonProperty("users__{0}__email")]
        public string Email { get; set; }
    }
}
