using Newtonsoft.Json;

namespace Splitwise.Requests.Expense;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class CreateEqualSplitExpenseRequest : BaseExpenseRequest
{
    public bool SplitEqually => true;

    public CreateEqualSplitExpenseRequest(decimal cost, string description, string currencyCode, int groupId)
        : base(cost, description, currencyCode)
    {
        GroupId = groupId;
    }
}