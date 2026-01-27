using System.Collections.Generic;

namespace Splitwise.Requests.Expense;

public class UpdateSharesSplitExpenseRequest : BaseSharesSplitExpenseRequest
{
    public UpdateSharesSplitExpenseRequest(decimal cost, string description, string currencyCode, IEnumerable<BasePaymentRequest> payments)
        : base(cost, description, currencyCode, payments)
    {
    }
}