namespace Splitwise.Responses.Shared;

public record BaseResponse
{
    public Errors Errors { get; init; }
}