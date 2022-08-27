namespace Splitwise.Requests.Expense
{
    public class CreateExpenseFromSentenceRequest
    {
        public CreateExpenseFromSentenceRequest(string input)
        {
            Input = input;
        }

        public string Input { get; }

        public int? FriendId { get; init; }

        public int? GroupId { get; init; }

        public bool Autosave { get; init; }
    }
}