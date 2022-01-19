namespace Splitwise.Requests.User
{
    public class UpdateUserRequest
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public string Locale { get; init; }

        public string DefaultCurrency { get; init; }
    }
}