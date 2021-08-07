namespace Splitwise.Responses.Shared
{
    public class BasePersonResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PictureResponse Picture { get; set; }
        
        public string Email { get; set; }

        public string RegistrationStatus { get; set; }
    }
}
