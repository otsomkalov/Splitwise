namespace Splitwise.Responses.Category
{
    public record BaseCategory
    {
        public int Id { get; init; }

        public string Name { get; init; }
    }
}