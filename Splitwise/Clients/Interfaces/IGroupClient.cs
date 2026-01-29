using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.Group;
using Splitwise.Responses.Group;

namespace Splitwise.Clients.Interfaces
{
    public interface IGroupClient
    {
        Task<ListGroupsResponse> ListAsync();

        Task<Group> GetAsync(int id);

        Task<Result<Group>> CreateAsync(CreateGroupRequest request);
    }
}