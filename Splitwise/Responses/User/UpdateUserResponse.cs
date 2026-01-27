namespace Splitwise.Responses.User
{
    public class UpdateUserResponse
    {
        public FullUser User { get; init; }

        public object Errors { get; init; }
    }
}