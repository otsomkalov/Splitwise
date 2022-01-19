using System;
using System.Collections.Generic;

namespace Splitwise.Responses.Category
{
    public record CategoryResponse
    {
        public IReadOnlyCollection<Category> Categories { get; init; } = Array.Empty<Category>();
    }
}