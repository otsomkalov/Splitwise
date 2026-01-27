namespace Splitwise.Tests.Settings;

public class ExpenseClientTestsSettings
{
    public const string SectionName = "Expense";

    public string ApiKey { get; init; }

    public long ExistingExpenseId { get; init; }

    public int FriendId { get; set; }

    public int CurrentUserId { get; set; }

    public int GroupId { get; set; }
}