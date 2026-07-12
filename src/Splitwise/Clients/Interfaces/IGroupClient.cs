using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.Group;
using Splitwise.Responses.Group;

namespace Splitwise.Clients.Interfaces
{
    /// <summary>
    /// Defines Splitwise group operations.
    /// </summary>
    public interface IGroupClient
    {
        /// <summary>
        /// Lists groups available to the authenticated user.
        /// </summary>
        /// <returns>A response containing groups.</returns>
        Task<ListGroupsResponse> ListAsync();

        /// <summary>
        /// Retrieves a group by identifier.
        /// </summary>
        /// <param name="id">Group identifier.</param>
        /// <returns>The requested group.</returns>
        Task<Group> GetAsync(int id);

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="request">Group creation payload.</param>
        /// <returns>A result containing the created group on success.</returns>
        Task<Result<Group>> CreateAsync(CreateGroupRequest request);

        /// <summary>
        /// Deletes a group by identifier.
        /// </summary>
        /// <param name="id">Group identifier.</param>
        /// <returns>A result indicating success state.</returns>
        Task<Result<bool>> DeleteAsync(int id);

        /// <summary>
        /// Restores a previously deleted group.
        /// </summary>
        /// <param name="id">Group identifier.</param>
        /// <returns>A result indicating success state.</returns>
        Task<Result<bool>> RestoreAsync(int id);

        /// <summary>
        /// Adds a user to a group.
        /// </summary>
        /// <param name="request">Add-user payload.</param>
        /// <returns>A result indicating success state.</returns>
        Task<Result<bool>> AddUserAsync(BaseAddUserToGroupRequest request);

        /// <summary>
        /// Removes a user from a group.
        /// </summary>
        /// <param name="request">Remove-user payload.</param>
        /// <returns>A result indicating success state.</returns>
        Task<Result<bool>> RemoveUserAsync(RemoveUserFromGroupRequest request);
    }
}