using System;
using System.Collections.Generic;
using Splitwise.Responses.Shared;

namespace Splitwise.Responses.Currency
{
    public record CurrenciesResponse(Errors Errors) : BaseResponse(Errors)
    {
        public IReadOnlyCollection<Currency> Currencies { get; init; } = Array.Empty<Currency>();
    }
}