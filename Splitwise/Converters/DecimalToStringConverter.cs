using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Splitwise.Converters;

public class DecimalToStringConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();

        if (decimal.TryParse(stringValue, out var value))
        {
            return value;
        }

        throw new JsonException($"Unable to convert \"{stringValue}\" to decimal.");
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("0.00"));
    }
}