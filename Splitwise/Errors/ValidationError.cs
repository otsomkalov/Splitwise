using FluentResults;

namespace Splitwise.Errors
{
    public class ValidationError : Error
    {
        public ValidationError(string propertyName, string errorMessage) : base($"{propertyName}:{errorMessage}")
        {
            
        }
    }
}
