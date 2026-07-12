using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.Friend;
using Splitwise.Responses.Friend;

namespace Splitwise.Clients.Interfaces
{
    /// <summary>
    /// Defines friend management operations in Splitwise.
    /// </summary>
    public interface IFriendClient
    {
        /// <summary>
        /// Retrieves the current user's friends.
        /// </summary>
        /// <returns>A read-only collection of friends.</returns>
        Task<IReadOnlyCollection<Friend>> ListAsync();

        /// <summary>
        /// Retrieves a friend by identifier.
        /// </summary>
        /// <param name="id">Friend identifier.</param>
        /// <returns>The requested friend.</returns>
        Task<Friend> GetAsync(int id);

        /// <summary>
        /// Adds a friend using friend-level details.
        /// </summary>
        /// <param name="request">Friend creation payload.</param>
        /// <returns>A result containing the created friend when successful.</returns>
        [Obsolete("Use by your own risk. Wasn't able to verify that request works using examples provided")]
        Task<Result<Friend>> AddAsync(AddFriendRequest request);

        /// <summary>
        /// Adds one or more friends in a single request.
        /// </summary>
        /// <param name="request">Batch friend creation payload.</param>
        /// <returns>A result containing created friends when successful.</returns>
        [Obsolete("Use by your own risk. Wasn't able to verify that request works using examples provided")]
        Task<Result<IReadOnlyCollection<Friend>>> AddAsync(AddFriendsRequest request);

        /// <summary>
        /// Deletes a friend relationship by identifier.
        /// </summary>
        /// <param name="id">Friend identifier.</param>
        /// <returns>A result indicating whether the deletion request succeeded.</returns>
        Task<Result> DeleteAsync(int id);
    }
}