using System.Collections.Generic;

namespace Splitwise.Requests.Expense;

public class CreateSharesSplitExpenseRequest : BaseSharesSplitExpenseRequest
{
    public CreateSharesSplitExpenseRequest(decimal cost, string description, string currencyCode, IEnumerable<BasePaymentRequest> payments)
        : base(cost, description, currencyCode, payments)
    {
    }
}