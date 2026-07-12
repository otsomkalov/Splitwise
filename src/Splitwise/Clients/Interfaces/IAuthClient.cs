using System.Threading.Tasks;
using Splitwise.Responses.Auth;

namespace Splitwise.Clients.Interfaces;

/// <summary>
/// Defines OAuth token exchange operations against Splitwise.
/// </summary>
public interface IAuthClient
{
    /// <summary>
    /// Exchanges an OAuth authorization code for an access token.
    /// </summary>
    /// <param name="clientId">OAuth application client ID.</param>
    /// <param name="clientSecret">OAuth application client secret.</param>
    /// <param name="code">Authorization code received from Splitwise OAuth flow.</param>
    /// <param name="redirectUrl">Redirect URI used during authorization.</param>
    /// <returns>OAuth token response containing an access token and related metadata.</returns>
    Task<TokenResponse> GetTokenAsync(string clientId, string clientSecret, string code, string redirectUrl);
}