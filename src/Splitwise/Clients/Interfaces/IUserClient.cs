using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.User;
using Splitwise.Responses.User;

namespace Splitwise.Clients.Interfaces
{
    /// <summary>
    /// Defines user profile operations in Splitwise.
    /// </summary>
    public interface IUserClient
    {
        /// <summary>
        /// Retrieves the currently authenticated user.
        /// </summary>
        /// <returns>The current user profile with full details.</returns>
        Task<FullUser> GetCurrentAsync();

        /// <summary>
        /// Retrieves a user by identifier.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <returns>The requested user.</returns>
        Task<User> GetAsync(int id);

        /// <summary>
        /// Performs a PATCH-like partial update of a user profile.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <param name="request">Partial user fields to update.</param>
        /// <returns>A result containing the updated user profile on success.</returns>
        Task<Result<FullUser>> UpdateAsync(int id, UpdateUserRequest request);
    }
}