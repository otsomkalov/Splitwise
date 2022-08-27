using FluentResults;

namespace Splitwise.Errors;

public class ForbiddenError : Error
{
    public ForbiddenError(string message) : base(message)
    {
    }
}