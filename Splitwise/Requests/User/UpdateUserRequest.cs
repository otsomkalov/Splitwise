namespace Splitwise.Requests.User
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Locale { get; set; }

        public string DefaultCurrency { get; set; }
    }
}