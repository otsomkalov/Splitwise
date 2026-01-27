using System;
using System.Collections.Generic;

namespace Splitwise.Responses.Category
{
    public record Category(
        string Icon,
        IconTypes IconTypes
    ) : BaseCategory
    {
        public IReadOnlyCollection<Category> Subcategories { get; init; } = Array.Empty<Category>();
    }
}