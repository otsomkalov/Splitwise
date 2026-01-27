using System.Collections.Generic;
using System.Threading.Tasks;
using Splitwise.Responses.Category;

namespace Splitwise.Clients.Interfaces
{
    public interface ICategoryClient
    {
        Task<IReadOnlyCollection<Category>> ListAsync();
    }
}