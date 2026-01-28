namespace Splitwise.Responses.User
{
    public class UpdateUserResponse
    {
        public FullUser User { get; init; }

        public Errors Errors { get; init; }
    }
}