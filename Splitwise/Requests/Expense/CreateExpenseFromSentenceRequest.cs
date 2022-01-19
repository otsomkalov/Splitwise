namespace Splitwise.Requests.Expense
{
    public class CreateExpenseFromSentenceRequest
    {
        public string Input { get; init; }

        public int? FriendId { get; init; }

        public int? GroupId { get; init; }

        public bool Autosave { get; init; }
    }
}