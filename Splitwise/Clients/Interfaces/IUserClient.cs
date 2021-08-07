using System.Threading.Tasks;
using Splitwise.Requests.User;
using Splitwise.Responses.User;

namespace Splitwise.Clients.Interfaces
{
    public interface IUserClient
    {
        Task<UserResponse> GetCurrentAsync();

        Task<UserResponse> GetAsync(int id);

        Task<UserResponse> UpdateAsync(int id, UpdateUserRequest request);
    }
}
