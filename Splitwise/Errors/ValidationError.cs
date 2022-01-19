using FluentResults;

namespace Splitwise.Errors
{
    public class ValidationError : Error
    {
        public ValidationError(string errorMessage) : base(errorMessage)
        {
        }
    }
}