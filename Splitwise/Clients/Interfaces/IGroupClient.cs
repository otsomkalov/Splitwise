using System.Threading.Tasks;
using Splitwise.Responses.Group;

namespace Splitwise.Clients.Interfaces
{
    public interface IGroupClient
    {
        Task<ListGroupsResponse> ListAsync();
    }
}