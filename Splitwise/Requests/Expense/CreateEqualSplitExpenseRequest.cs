namespace Splitwise.Requests.Expense;

public class CreateEqualSplitExpenseRequest : BaseExpenseRequest
{
    public bool SplitEqually => true;

    public CreateEqualSplitExpenseRequest(decimal cost, string description, string currencyCode, int groupId)
        : base(cost, description, currencyCode)
    {
        GroupId = groupId;
    }
}