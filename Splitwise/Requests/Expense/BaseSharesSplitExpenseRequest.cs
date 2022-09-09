using System.Collections.Generic;
using Newtonsoft.Json;

namespace Splitwise.Requests.Expense;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public abstract class BaseSharesSplitExpenseRequest : BaseExpenseRequest
{
    protected BaseSharesSplitExpenseRequest(decimal cost, string description, string currencyCode, IEnumerable<BasePaymentRequest> payments)
        : base(cost, description, currencyCode)
    {
        Payments = payments;
    }

    [JsonIgnore]
    public IEnumerable<BasePaymentRequest> Payments { get; }
}