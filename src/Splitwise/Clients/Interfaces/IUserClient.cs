using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.User;
using Splitwise.Responses.User;

namespace Splitwise.Clients.Interfaces
{
    public interface IUserClient
    {
        Task<FullUser> GetCurrentAsync();

        Task<User> GetAsync(int id);

        /// <summary>
        /// Performs PATCH-like update of the user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result<FullUser>> UpdateAsync(int id, UpdateUserRequest request);
    }
}