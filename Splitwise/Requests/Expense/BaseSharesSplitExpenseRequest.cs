using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Splitwise.Requests.Expense;

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