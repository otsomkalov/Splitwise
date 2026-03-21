using FluentResults;

namespace Splitwise.Errors;

public class UnknownError : Error
{
    public UnknownError(string message) : base(message)
    {
    }
}