using System.Threading.Tasks;
using Splitwise.Responses.Auth;

namespace Splitwise.Clients.Interfaces;

public interface IAuthClient
{
    Task<TokenResponse> GetTokenAsync(string clientId, string clientSecret, string code, string redirectUrl);
}