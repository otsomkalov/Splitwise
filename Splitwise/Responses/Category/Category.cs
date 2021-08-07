using System.Collections.Generic;

namespace Splitwise.Responses.Category
{
    public record Category(
        int Id,
        string Name,
        string Icon,
        IconTypes IconTypes,
        IEnumerable<Category> Subcategories
    );
}