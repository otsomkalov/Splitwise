using System.Threading.Tasks;
using Splitwise.Responses.Currency;

namespace Splitwise.Clients.Interfaces
{
    public interface ICurrencyClient
    {
        Task<CurrenciesResponse> ListAsync();
    }
}
