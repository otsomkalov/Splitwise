using System.Collections.Generic;
using System.Threading.Tasks;
using Splitwise.Responses.Category;

namespace Splitwise.Clients.Interfaces
{
    /// <summary>
    /// Defines operations for retrieving category metadata from Splitwise.
    /// </summary>
    public interface ICategoryClient
    {
        /// <summary>
        /// Retrieves all expense categories available for the authenticated context.
        /// </summary>
        /// <returns>A read-only collection of categories.</returns>
        Task<IReadOnlyCollection<Category>> ListAsync();
    }
}