namespace Splitwise.Clients.Interfaces
{
    public interface ISplitwiseClient : IAnonymousSplitwiseClient
    {
        IUserClient User { get; }
        IFriendClient Friend { get; }
    }
}
