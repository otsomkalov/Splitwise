using Splitwise.Responses.Shared;

namespace Splitwise.Responses.User
{
    public record BaseUser
    {
        public string FirstName { get; init; }

        public int Id { get; init; }

        public string LastName { get; init; }

        public Picture Picture { get; init; }
    }
}