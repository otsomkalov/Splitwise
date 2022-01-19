using System;
using System.Collections.Generic;
using Splitwise.Responses.Expense;
using Splitwise.Responses.Shared;
using Splitwise.Responses.User;

namespace Splitwise.Responses.Group
{
    public record Group
    {
        public FullPicture Avatar { get; init; }

        public LargePicture CoverPhoto { get; init; }

        public DateTime CreatedAt { get; init; }

        public bool CustomAvatar { get; init; }

        public GroupType GroupType { get; init; }

        public int Id { get; init; }

        public string InviteLink { get; init; }

        public IReadOnlyCollection<GroupMember> Members { get; init; }

        public IReadOnlyCollection<FullPayment> OriginalDebts { get; init; }

        public IReadOnlyCollection<FullPayment> SimplifiedDebts { get; init; }

        public string Name { get; init; }

        public bool SimplifyByDefault { get; init; }

        public LargePicture TallAvatar { get; init; }

        public DateTime UpdatedAt { get; init; }

        public string Whiteboard { get; init; }
    }

    public enum GroupType
    {
        Apartment,
        Home,
        Trip,
        Other
    }
}