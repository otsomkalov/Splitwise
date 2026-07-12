namespace Splitwise.Clients.Interfaces
{
    /// <summary>
    /// Provides access to Splitwise API clients that do not require an authenticated user API key.
    /// </summary>
    public interface IAnonymousSplitwiseClient
    {
        /// <summary>
        /// Gets a client for <c>get_currencies</c> operations.
        /// </summary>
        ICurrencyClient Currency { get; }

        /// <summary>
        /// Gets a client for <c>get_categories</c> operations.
        /// </summary>
        ICategoryClient Category { get; }

        /// <summary>
        /// Gets a client for OAuth token exchange operations.
        /// </summary>
        IAuthClient Auth { get; }
    }
}