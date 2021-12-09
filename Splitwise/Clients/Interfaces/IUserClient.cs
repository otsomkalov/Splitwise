using System.Threading.Tasks;
using Splitwise.Requests.User;
using Splitwise.Responses.Shared;
using Splitwise.Responses.User;

namespace Splitwise.Clients.Interfaces
{
    public interface IUserClient
    {
        Task<CurrentUserResponse> GetCurrentAsync();

        Task<BasePersonResponse> GetAsync(int id);

        Task<CurrentUserResponse> UpdateAsync(int id, UpdateUserRequest request);
    }
}
