namespace Splitwise.Responses.Expense
{
    public record FullPayment(
        string CurrencyCode
    ) : Payment;
}