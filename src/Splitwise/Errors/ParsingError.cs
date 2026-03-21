using System.Collections.Generic;
using FluentResults;

namespace Splitwise.Errors;

public class ParsingError : Error
{
    public ParsingError(string propertyName, IEnumerable<string> errors)
        : base($"{propertyName}: {string.Join(",", errors)}")
    {
    }
}