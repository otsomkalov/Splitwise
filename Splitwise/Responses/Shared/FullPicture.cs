namespace Splitwise.Responses.Shared
{
    public record FullPicture : Picture
    {
        public string Original { get; init; }

        public string Xxlarge { get; init; }

        public string Xlarge { get; init; }
    }
}