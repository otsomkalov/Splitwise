namespace Splitwise.Responses.User
{
    public record User : BaseUser
    {
        public bool CustomPicture { get; init; }

        public string Email { get; init; }

        public string RegistrationStatus { get; init; }
    }
}