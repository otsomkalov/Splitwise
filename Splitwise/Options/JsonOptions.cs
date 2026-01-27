using System.Text.Json;
using System.Text.Json.Serialization;
using Splitwise.Converters;
using Splitwise.Requests.Expense;

namespace Splitwise.Options
{
    public static class JsonOptions
    {
        public static readonly JsonSerializerOptions JsonSerializerSettings = new ()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters =
            {
                new JsonStringEnumConverter<RepeatInterval>(JsonNamingPolicy.CamelCase),
                new DecimalToStringConverter()
            }
        };
    }
}