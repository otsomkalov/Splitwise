using System;
using System.Collections.Generic;
using Splitwise.Responses.Shared;
using Splitwise.Responses.User;

namespace Splitwise.Responses.Group
{
    public class GroupResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public GroupType GroupType { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool SimplifyByDefault { get; set; }

        public IReadOnlyCollection<CurrentUserResponse> Members { get; set; }

        public PictureResponse Avatar { get; set; }

        public bool CustomAvatar { get; set; }

        public PictureResponse CoverPhoto { get; set; }

        public string InviteLink { get; set; }
    }

    public enum GroupType
    {
        Apartment
    }
}