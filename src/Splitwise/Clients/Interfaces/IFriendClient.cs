using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using Splitwise.Requests.Friend;
using Splitwise.Responses.Friend;

namespace Splitwise.Clients.Interfaces
{
    public interface IFriendClient
    {
        Task<IReadOnlyCollection<Friend>> ListAsync();

        Task<Friend> GetAsync(int id);

        [Obsolete("Use by your own risk. Wasn't able to verify that request works using examples provided")]
        Task<Result<Friend>> AddAsync(AddFriendRequest request);

        [Obsolete("Use by your own risk. Wasn't able to verify that request works using examples provided")]
        Task<Result<IReadOnlyCollection<Friend>>> AddAsync(AddFriendsRequest request);

        Task<Result> DeleteAsync(int id);
    }
}