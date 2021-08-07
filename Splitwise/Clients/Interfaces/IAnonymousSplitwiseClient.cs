namespace Splitwise.Clients.Interfaces
{
    public interface IAnonymousSplitwiseClient
    {
        ICurrencyClient Currency { get; }

        ICategoryClient Category { get; }
    }
}