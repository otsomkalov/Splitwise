using System;
using System.Collections.Generic;

namespace Splitwise.Responses.Category
{
    internal record CategoryResponse
    {
        public IReadOnlyCollection<Category> Categories { get; init; } = Array.Empty<Category>();
    }
}