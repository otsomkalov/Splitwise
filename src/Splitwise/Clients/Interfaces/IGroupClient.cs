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

        Task<Result<bool>> DeleteAsync(int id);

        Task<Result<bool>> RestoreAsync(int id);

        Task<Result<bool>> AddUserAsync(BaseAddUserToGroupRequest request);

        Task<Result<bool>> RemoveUserAsync(RemoveUserFromGroupRequest request);
    }
}