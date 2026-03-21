using System.Collections.Generic;
using System.Threading.Tasks;
using Splitwise.Responses.Currency;

namespace Splitwise.Clients.Interfaces
{
    public interface ICurrencyClient
    {
        Task<IReadOnlyCollection<Currency>> ListAsync();
    }
}