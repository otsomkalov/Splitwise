namespace Splitwise.Responses.Expense
{
    public record Payment
    {
        public int From { get; init; }

        public int To { get; init; }

        public decimal Amount { get; init; }
    }
}