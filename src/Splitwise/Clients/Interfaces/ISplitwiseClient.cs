namespace Splitwise.Clients.Interfaces
{
    /// <summary>
    /// Provides access to Splitwise API clients that require an authenticated user API key.
    /// </summary>
    public interface ISplitwiseClient : IAnonymousSplitwiseClient
    {
        /// <summary>
        /// Gets the user profile client.
        /// </summary>
        IUserClient User { get; }

        /// <summary>
        /// Gets the friend management client.
        /// </summary>
        IFriendClient Friend { get; }

        /// <summary>
        /// Gets the group management client.
        /// </summary>
        IGroupClient Group { get; }

        /// <summary>
        /// Gets the expense management client.
        /// </summary>
        IExpenseClient Expense { get; }
    }
}