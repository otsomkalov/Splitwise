using Newtonsoft.Json;

namespace Splitwise.Requests.Expense
{
    public class ExistingUserPayment : BasePaymentRequest
    {
        [JsonProperty("users__{0}__user_id")]
        public int? UserId { get; init; }
    }
}