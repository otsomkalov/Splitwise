using System;
using System.Collections.Generic;
using Splitwise.Responses.Shared;

namespace Splitwise.Responses.Friend
{
    public record Friend
    (
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string RegistrationStatus,
        PictureResponse Picture,
        List<BalanceResponse> Balance,
        List<GroupResponse> Groups,
        DateTime UpdatedAt
    );
}