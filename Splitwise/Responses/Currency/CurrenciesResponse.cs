using System;
using System.Collections.Generic;
using Splitwise.Responses.Shared;

namespace Splitwise.Responses.Currency
{
    public record CurrenciesResponse
    {
        public IReadOnlyCollection<Currency> Currencies { get; init; } = Array.Empty<Currency>();
    }
}