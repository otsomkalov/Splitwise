using System.Collections.Generic;
using System.Threading.Tasks;
using Splitwise.Responses.Currency;

namespace Splitwise.Clients.Interfaces
{
    /// <summary>
    /// Defines operations for retrieving currency metadata from Splitwise.
    /// </summary>
    public interface ICurrencyClient
    {
        /// <summary>
        /// Retrieves currencies supported by Splitwise.
        /// </summary>
        /// <returns>A read-only collection of currencies.</returns>
        Task<IReadOnlyCollection<Currency>> ListAsync();
    }
}